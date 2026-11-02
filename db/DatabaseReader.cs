using Microsoft.Data.Sqlite;
using System.IO;

namespace System_rezerwacji_miejsc.db;

// odczyt danych z bazy
// wszystkie metody uzywaja parametry SQL zeby uniknac SQL Injection
public class DatabaseReader
{
    private readonly string connectionString;

    public DatabaseReader()
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        Directory.CreateDirectory(dataFolder);
        string dbPath = Path.Combine(dataFolder, "kino.db");
        connectionString = $"Data Source={dbPath}";
    }

    // pobiera wszystkie filmy jako obiekty
    public List<Film> PobierzWszystkieFilmy()
    {
        var filmy = new List<Film>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT Id, Tytul, Opis, CzasTrwania, ImageURL FROM Filmy ORDER BY Id;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            filmy.Add(new Film
            {
                Id = czytnik.GetInt32(0),
                Tytul = czytnik.GetString(1),
                Opis = czytnik.IsDBNull(2) ? "" : czytnik.GetString(2),
                CzasTrwania = czytnik.GetInt32(3),
                ImageURL = czytnik.IsDBNull(4) ? "" : czytnik.GetString(4)
            });
        }

        return filmy;
    }

    // pobiera tylko tytuly filmow
    public List<string> PobierzFilmy()
    {
        var filmy = new List<string>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT Tytul FROM Filmy ORDER BY Id;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            filmy.Add(czytnik.GetString(0));
        }

        return filmy;
    }

    // pobiera seanse dla konkretnego filmu
    public List<Seans> PobierzSeanseDlaFilmu(int filmId)
    {
        var seanse = new List<Seans>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT Id, FilmId, DataGodzina, Sala FROM Seanse WHERE FilmId = @filmId ORDER BY DataGodzina;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@filmId", filmId);

        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            seanse.Add(new Seans
            {
                Id = czytnik.GetInt32(0),
                FilmId = czytnik.GetInt32(1),
                DataGodzina = czytnik.GetString(2),
                Sala = czytnik.GetInt32(3)
            });
        }

        return seanse;
    }

    // pobiera tylko daty wszystkich seansow
    public List<string> PobierzSeanse()
    {
        var seanse = new List<string>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT DataGodzina FROM Seanse ORDER BY DataGodzina;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            seanse.Add(czytnik.GetString(0));
        }

        return seanse;
    }

    // pobiera wszystkie miejsca w danej sali jako obiekty
    public List<Miejsce> PobierzObiektyMiejsc(int sala)
    {
        var miejsca = new List<Miejsce>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT Id, Sala, Rzad, NumerMiejsca FROM Miejsca WHERE Sala = @sala ORDER BY NumerMiejsca;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@sala", sala);

        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            miejsca.Add(new Miejsce
            {
                Id = czytnik.GetInt32(0),
                Sala = czytnik.GetInt32(1),
                Rzad = czytnik.GetInt32(2),
                NumerMiejsca = czytnik.GetInt32(3)
            });
        }

        return miejsca;
    }

    // pobiera tylko numery miejsc w sali
    public List<int> PobierzMiejsca(int sala)
    {
        var miejsca = new List<int>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT NumerMiejsca FROM Miejsca WHERE Sala = @sala ORDER BY NumerMiejsca;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@sala", sala);

        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            miejsca.Add(czytnik.GetInt32(0));
        }

        return miejsca;
    }

    // pobiera id miejsc juz zarezerwowanych na dany seans
    public List<int> PobierzZarezerwowaneMiejsca(int seansId)
    {
        var zajeteMiejsca = new List<int>();

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT MiejsceId FROM Rezerwacje WHERE SeansId = @seansId;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@seansId", seansId);

        using var czytnik = polecenie.ExecuteReader();
        while (czytnik.Read())
        {
            zajeteMiejsca.Add(czytnik.GetInt32(0));
        }

        return zajeteMiejsca;
    }

    // pobiera url obrazka dla filmu
    public string PobierzObrazFilmu(int filmId)
    {
        string imageURL = "";

        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = "SELECT ImageURL FROM Filmy WHERE Id = @filmId;";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@filmId", filmId);

        object? wynik = polecenie.ExecuteScalar();
        if (wynik != null && wynik != DBNull.Value)
        {
            imageURL = wynik.ToString() ?? "";
        }

        return imageURL;
    }
}
