using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace System_rezerwacji_miejsc.db;

// eksport i import danych do CSV i JSON
// pozwala na backup rezerwacji oraz wymiane danych z innymi systemami
public class EksportImport
{
    private readonly string connectionString;

    public EksportImport()
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        Directory.CreateDirectory(dataFolder);
        string dbPath = Path.Combine(dataFolder, "kino.db");
        connectionString = $"Data Source={dbPath}";
    }

    // =========================================================================
    // EKSPORT DO CSV
    // =========================================================================

    // eksportuje wszystkie filmy do pliku CSV
    // format: Id,Tytul,Opis,CzasTrwania,ImageURL
    public void EksportujFilmyDoCsv(string sciezka)
    {
        var filmy = new DatabaseReader().PobierzWszystkieFilmy();

        var sb = new StringBuilder();
        // naglowek pliku
        sb.AppendLine("Id,Tytul,Opis,CzasTrwania,ImageURL");
        foreach (var f in filmy)
        {
            string opis = CsvEsc(f.Opis);
            string tytul = CsvEsc(f.Tytul);
            string url = CsvEsc(f.ImageURL);
            sb.AppendLine($"{f.Id},{tytul},{opis},{f.CzasTrwania},{url}");
        }

        File.WriteAllText(sciezka, sb.ToString(), Encoding.UTF8);
    }

    // eksportuje wszystkie rezerwacje (zlaczone z seansami i miejscami) do CSV
    // format: RezerwacjaId,FilmTytul,DataGodzina,Sala,Rzad,NumerMiejsca
    public void EksportujRezerwacjeDoCsv(string sciezka)
    {
        var sb = new StringBuilder();
        sb.AppendLine("RezerwacjaId,FilmTytul,DataGodzina,Sala,Rzad,NumerMiejsca");

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        // zlaczenie 4 tabel zeby dostac czytelny raport
        string sql = @"
            SELECT r.Id, f.Tytul, s.DataGodzina, s.Sala, m.Rzad, m.NumerMiejsca
            FROM Rezerwacje r
            JOIN Seanse s ON r.SeansId = s.Id
            JOIN Filmy f ON s.FilmId = f.Id
            JOIN Miejsca m ON r.MiejsceId = m.Id
            ORDER BY r.Id;
        ";

        using var cmd = new SqliteCommand(sql, polaczenie);
        using var czytnik = cmd.ExecuteReader();
        while (czytnik.Read())
        {
            int id = czytnik.GetInt32(0);
            string tytul = CsvEsc(czytnik.GetString(1));
            string data = CsvEsc(czytnik.GetString(2));
            int sala = czytnik.GetInt32(3);
            int rzad = czytnik.GetInt32(4);
            int numer = czytnik.GetInt32(5);
            sb.AppendLine($"{id},{tytul},{data},{sala},{rzad},{numer}");
        }

        File.WriteAllText(sciezka, sb.ToString(), Encoding.UTF8);
    }

    // =========================================================================
    // EKSPORT DO JSON
    // =========================================================================

    // eksportuje wszystkie dane do jednego pliku JSON
    public void EksportujWszystkoDoJson(string sciezka)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        // struktura dokumentu z data eksportu
        var dokument = new
        {
            DataEksportu = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            Filmy = PobierzFilmy(polaczenie),
            Seanse = PobierzSeanse(polaczenie),
            Miejsca = PobierzMiejsca(polaczenie),
            Rezerwacje = PobierzRezerwacje(polaczenie)
        };

        // wyciagamy polskie znaki zamiast \uXXXX
        var opcje = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        string json = JsonSerializer.Serialize(dokument, opcje);
        File.WriteAllText(sciezka, json, Encoding.UTF8);
    }

    // =========================================================================
    // IMPORT Z CSV
    // =========================================================================

    // importuje filmy z pliku CSV
    // pomija filmy o juz istniejacym tytule
    // zwraca liczbe zaimportowanych rekordow
    public int ImportujFilmyZCsv(string sciezka)
    {
        if (!File.Exists(sciezka))
        {
            throw new FileNotFoundException($"Plik CSV nie istnieje: {sciezka}");
        }

        string[] linie = File.ReadAllLines(sciezka, Encoding.UTF8);
        if (linie.Length < 2) return 0;

        int licznik = 0;
        var writer = new DatabaseWriter();
        var reader = new DatabaseReader();
        var istniejace = reader.PobierzWszystkieFilmy();
        // zbior tytulow dla szybkiego sprawdzania duplikatow
        var istniejaceTytuly = istniejace.Select(f => f.Tytul.ToLowerInvariant()).ToHashSet();

        // pomijamy naglowek w pierwszej linii
        for (int i = 1; i < linie.Length; i++)
        {
            string linia = linie[i];
            if (string.IsNullOrWhiteSpace(linia)) continue;

            string[] pola = ParsujWierszCsv(linia);
            if (pola.Length < 5) continue;

            // pomijamy jesli film o tym tytule juz istnieje
            string tytul = pola[1];
            if (istniejaceTytuly.Contains(tytul.ToLowerInvariant())) continue;

            string opis = pola[2];
            if (!int.TryParse(pola[3], out int czasTrwania)) continue;
            string url = pola[4];

            writer.DodajFilm(tytul, opis, czasTrwania, url);
            istniejaceTytuly.Add(tytul.ToLowerInvariant());
            licznik++;
        }

        return licznik;
    }

    // =========================================================================
    // METODY POMOCNICZE
    // =========================================================================

    // escapuje wartosc do CSV - cudzyslowy jesli jest przecinek lub cudzyslow
    private static string CsvEsc(string wartosc)
    {
        if (string.IsNullOrEmpty(wartosc)) return "";
        if (wartosc.Contains(',') || wartosc.Contains('"') || wartosc.Contains('\n'))
        {
            return "\"" + wartosc.Replace("\"", "\"\"") + "\"";
        }
        return wartosc;
    }

    // parsuje wiersz CSV - obsluguje cudzyslowy i przecinki wewnatrz pol
    private static string[] ParsujWierszCsv(string linia)
    {
        var pola = new List<string>();
        var sb = new StringBuilder();
        bool wCudzyslowie = false;

        for (int i = 0; i < linia.Length; i++)
        {
            char c = linia[i];
            if (c == '"')
            {
                // podwojny cudzyslow = escaped cudzyslow
                if (wCudzyslowie && i + 1 < linia.Length && linia[i + 1] == '"')
                {
                    sb.Append('"');
                    i++;
                }
                else
                {
                    wCudzyslowie = !wCudzyslowie;
                }
            }
            else if (c == ',' && !wCudzyslowie)
            {
                // koniec pola
                pola.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
        }
        // ostatnie pole
        pola.Add(sb.ToString());
        return pola.ToArray();
    }

    // pobiera filmy do JSON
    private static List<object> PobierzFilmy(SqliteConnection polaczenie)
    {
        var lista = new List<object>();
        string sql = "SELECT Id, Tytul, Opis, CzasTrwania, ImageURL FROM Filmy ORDER BY Id;";
        using var cmd = new SqliteCommand(sql, polaczenie);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            lista.Add(new
            {
                Id = r.GetInt32(0),
                Tytul = r.GetString(1),
                Opis = r.IsDBNull(2) ? "" : r.GetString(2),
                CzasTrwania = r.GetInt32(3),
                ImageURL = r.IsDBNull(4) ? "" : r.GetString(4)
            });
        }
        return lista;
    }

    // pobiera seanse do JSON
    private static List<object> PobierzSeanse(SqliteConnection polaczenie)
    {
        var lista = new List<object>();
        string sql = "SELECT Id, FilmId, DataGodzina, Sala FROM Seanse ORDER BY Id;";
        using var cmd = new SqliteCommand(sql, polaczenie);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            lista.Add(new
            {
                Id = r.GetInt32(0),
                FilmId = r.GetInt32(1),
                DataGodzina = r.GetString(2),
                Sala = r.GetInt32(3)
            });
        }
        return lista;
    }

    // pobiera miejsca do JSON
    private static List<object> PobierzMiejsca(SqliteConnection polaczenie)
    {
        var lista = new List<object>();
        string sql = "SELECT Id, Sala, Rzad, NumerMiejsca FROM Miejsca ORDER BY Id;";
        using var cmd = new SqliteCommand(sql, polaczenie);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            lista.Add(new
            {
                Id = r.GetInt32(0),
                Sala = r.GetInt32(1),
                Rzad = r.GetInt32(2),
                NumerMiejsca = r.GetInt32(3)
            });
        }
        return lista;
    }

    // pobiera rezerwacje do JSON
    private static List<object> PobierzRezerwacje(SqliteConnection polaczenie)
    {
        var lista = new List<object>();
        string sql = "SELECT Id, SeansId, MiejsceId FROM Rezerwacje ORDER BY Id;";
        using var cmd = new SqliteCommand(sql, polaczenie);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            lista.Add(new
            {
                Id = r.GetInt32(0),
                SeansId = r.GetInt32(1),
                MiejsceId = r.GetInt32(2)
            });
        }
        return lista;
    }
}
