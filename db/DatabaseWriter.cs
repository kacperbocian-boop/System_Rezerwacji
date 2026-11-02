using Microsoft.Data.Sqlite;
using System.IO;

namespace System_rezerwacji_miejsc.db;

// zapis i usuwanie danych w bazie
// wszystkie metody uzywaja parametry SQL zeby uniknac SQL Injection
public class DatabaseWriter
{
    private readonly string connectionString;

    public DatabaseWriter()
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        Directory.CreateDirectory(dataFolder);
        string dbPath = Path.Combine(dataFolder, "kino.db");
        connectionString = $"Data Source={dbPath}";
    }

    // dodaje nowy film do bazy
    public void DodajFilm(string tytul, string opis, int czasTrwania, string imageURL)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = @"
            INSERT INTO Filmy (Tytul, Opis, CzasTrwania, ImageURL)
            VALUES (@tytul, @opis, @czasTrwania, @imageURL);
        ";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@tytul", tytul);
        polecenie.Parameters.AddWithValue("@opis", opis);
        polecenie.Parameters.AddWithValue("@czasTrwania", czasTrwania);
        polecenie.Parameters.AddWithValue("@imageURL", imageURL);
        polecenie.ExecuteNonQuery();
    }

    // dodaje nowy seans
    public void DodajSeans(int filmId, string dataGodzina, int sala)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = @"
            INSERT INTO Seanse (FilmId, DataGodzina, Sala)
            VALUES (@filmId, @dataGodzina, @sala);
        ";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@filmId", filmId);
        polecenie.Parameters.AddWithValue("@dataGodzina", dataGodzina);
        polecenie.Parameters.AddWithValue("@sala", sala);
        polecenie.ExecuteNonQuery();
    }

    // dodaje nowe miejsce w sali
    public void DodajMiejsce(int sala, int rzad, int numerMiejsca)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = @"
            INSERT INTO Miejsca (Sala, Rzad, NumerMiejsca)
            VALUES (@sala, @rzad, @numerMiejsca);
        ";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@sala", sala);
        polecenie.Parameters.AddWithValue("@rzad", rzad);
        polecenie.Parameters.AddWithValue("@numerMiejsca", numerMiejsca);
        polecenie.ExecuteNonQuery();
    }

    // dodaje rezerwacje miejsca na seans
    public void DodajRezerwacje(int seansId, int miejsceId)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = @"
            INSERT INTO Rezerwacje (SeansId, MiejsceId)
            VALUES (@seansId, @miejsceId);
        ";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@seansId", seansId);
        polecenie.Parameters.AddWithValue("@miejsceId", miejsceId);
        polecenie.ExecuteNonQuery();
    }

    // usuwa pojedyncza rezerwacje
    public void UsunRezerwacje(int seansId, int miejsceId)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = @"
            DELETE FROM Rezerwacje
            WHERE SeansId = @seansId AND MiejsceId = @miejsceId;
        ";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@seansId", seansId);
        polecenie.Parameters.AddWithValue("@miejsceId", miejsceId);
        polecenie.ExecuteNonQuery();
    }

    // usuwa wszystkie rezerwacje na dany seans
    public void UsunWszystkieRezerwacje(int seansId)
    {
        using var polaczenie = new SqliteConnection(connectionString);
        polaczenie.Open();

        string sql = @"
            DELETE FROM Rezerwacje
            WHERE SeansId = @seansId;
        ";

        using var polecenie = new SqliteCommand(sql, polaczenie);
        polecenie.Parameters.AddWithValue("@seansId", seansId);
        polecenie.ExecuteNonQuery();
    }
}
