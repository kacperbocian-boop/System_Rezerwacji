using Microsoft.Data.Sqlite;
using System.IO;

namespace System_rezerwacji_miejsc.db;

// klasa do inicjalizacji bazy danych
// tworzy tabele i wypelnia danymi startowymi
public class Database
{
    // connection string do bazy
    private readonly string connectionString;

    public Database()
    {
        // baza w podkatalogu data obok exe
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        Directory.CreateDirectory(dataFolder);
        string dbPath = Path.Combine(dataFolder, "kino.db");
        connectionString = $"Data Source={dbPath}";
    }

    // zwraca nowe polaczenie do bazy
    public SqliteConnection PobierzPolaczenie()
    {
        return new SqliteConnection(connectionString);
    }

    // tworzy baze jesli nie istnieje
    public void UtworzBaze()
    {
        using (var polaczenie = PobierzPolaczenie())
        {
            polaczenie.Open();

            // wszystkie 4 tabele
            string sql = @"
                CREATE TABLE IF NOT EXISTS Filmy (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Tytul TEXT NOT NULL,
                    Opis TEXT,
                    CzasTrwania INTEGER NOT NULL,
                    ImageURL TEXT
                );

                CREATE TABLE IF NOT EXISTS Seanse (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FilmId INTEGER NOT NULL,
                    DataGodzina TEXT NOT NULL,
                    Sala INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Miejsca (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Sala INTEGER NOT NULL,
                    Rzad INTEGER NOT NULL,
                    NumerMiejsca INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Rezerwacje (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SeansId INTEGER NOT NULL,
                    MiejsceId INTEGER NOT NULL
                );
            ";

            using (var polecenie = polaczenie.CreateCommand())
            {
                polecenie.CommandText = sql;
                polecenie.ExecuteNonQuery();
            }

            // sprawdzamy czy sa filmy w bazie
            // jak nie to wypelniamy danymi startowymi
            using (var sprCmd = polaczenie.CreateCommand())
            {
                sprCmd.CommandText = "SELECT COUNT(*) FROM Filmy;";
                long ileFilmow = (long)(sprCmd.ExecuteScalar() ?? 0L);
                if (ileFilmow == 0)
                {
                    WypelnijDanymiPoczatkowymi(polaczenie);
                }
            }
        }
    }

    // wstawia dane startowe do pustej bazy
    private void WypelnijDanymiPoczatkowymi(SqliteConnection polaczenie)
    {
        // 4 filmy
        string sqlFilmy = @"
            INSERT INTO Filmy (Tytul, Opis, CzasTrwania, ImageURL) VALUES
            ('Incepcja', 'Doświadczony złodziej wykrada sekrety z ludzkich snów. Dostaje zadanie zaszczepienia myśli w umyśle bogatego dziedzica.', 148, 'https://upload.wikimedia.org/wikipedia/en/2/2e/Inception_%282010%29_theatrical_poster.jpg'),
            ('Interstellar', 'Grupa badaczy wyrusza w podróż przez tunel czasoprzestrzenny w poszukiwaniu nowego domu dla ludzkości.', 169, 'https://upload.wikimedia.org/wikipedia/en/b/bc/Interstellar_film_poster.jpg'),
            ('Matrix', 'Haker Neo dowiaduje się od tajemniczych rebeliantów prawdy o swoim świecie oraz o swojej roli w wojnie z maszynami.', 136, 'https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg'),
            ('Mroczny Rycerz', 'Batman wraz z policją i prokuratorem stara się zwalczać przestępczość w Gotham. Pojawia się psychopatyczny Joker.', 152, 'https://upload.wikimedia.org/wikipedia/en/1/1c/The_Dark_Knight_%282008_film%29.jpg');
        ";

        using (var cmd = polaczenie.CreateCommand())
        {
            cmd.CommandText = sqlFilmy;
            cmd.ExecuteNonQuery();
        }

        // 8 seansow po 2 na film
        string sqlSeanse = @"
            INSERT INTO Seanse (FilmId, DataGodzina, Sala) VALUES
            (1, '2026-11-05 17:00', 1),
            (1, '2026-11-05 20:30', 1),
            (2, '2026-11-06 18:00', 1),
            (2, '2026-11-06 21:15', 1),
            (3, '2026-11-07 16:30', 2),
            (3, '2026-11-07 19:30', 2),
            (4, '2026-11-08 17:00', 2),
            (4, '2026-11-08 20:15', 2);
        ";

        using (var cmd = polaczenie.CreateCommand())
        {
            cmd.CommandText = sqlSeanse;
            cmd.ExecuteNonQuery();
        }

        // 32 miejsca na sale (4 rzeddy po 8 foteli)
        for (int sala = 1; sala <= 2; sala++)
        {
            for (int numer = 1; numer <= 32; numer++)
            {
                int rzad = ((numer - 1) / 8) + 1;
                using var cmd = polaczenie.CreateCommand();
                cmd.CommandText = "INSERT INTO Miejsca (Sala, Rzad, NumerMiejsca) VALUES (@sala, @rzad, @numer);";
                cmd.Parameters.AddWithValue("@sala", sala);
                cmd.Parameters.AddWithValue("@rzad", rzad);
                cmd.Parameters.AddWithValue("@numer", numer);
                cmd.ExecuteNonQuery();
            }
        }

        // 3 przykladowe rezerwacje na pierwszy seans
        string sqlRezerwacje = @"
            INSERT INTO Rezerwacje (SeansId, MiejsceId) VALUES
            (1, 10),
            (1, 11),
            (1, 12);
        ";

        using (var cmd = polaczenie.CreateCommand())
        {
            cmd.CommandText = sqlRezerwacje;
            cmd.ExecuteNonQuery();
        }
    }
}
