using System_rezerwacji_miejsc.db;

namespace System_rezerwacji_miejsc.Logika;

// walidacja danych wejsciowych przed rezerwacja
// metody nie zaleza od stanu wiec latwe do testowania
public class Walidacja
{
    // sprawdza czy koszyk jest poprawny przed zatwierdzeniem
    // zwraca komunikat bledu albo pusty string jak ok
    public string SprawdzKoszyk(Koszyk koszyk, Seans wybranySeans)
    {
        if (wybranySeans is null)
        {
            return "Nie wybrano seansu. Wybierz film i seans z listy.";
        }

        if (koszyk is null)
        {
            return "Koszyk nie został zainicjowany.";
        }

        if (koszyk.Policz() == 0)
        {
            return "Koszyk jest pusty. Zaznacz przynajmniej jedno wolne miejsce na sali.";
        }

        // sprawdzamy czy wszystkie pozycje dotycza tego samego seansu
        var pozycjeDlaSeansu = koszyk.PobierzDlaSeansu(wybranySeans.Id);
        if (pozycjeDlaSeansu.Count != koszyk.Policz())
        {
            return "Koszyk zawiera pozycje z różnych seansów. Wyczyść koszyk i wybierz miejsca ponownie.";
        }

        return "";
    }

    // walidacja danych filmu przy dodawaniu nowego
    public string SprawdzFilm(string tytul, int czasTrwania)
    {
        if (string.IsNullOrWhiteSpace(tytul))
        {
            return "Tytuł filmu nie może być pusty.";
        }

        if (tytul.Length > 200)
        {
            return "Tytuł filmu jest zbyt długi (maks. 200 znaków).";
        }

        if (czasTrwania <= 0)
        {
            return "Czas trwania musi być większy od zera.";
        }

        if (czasTrwania > 600)
        {
            return "Czas trwania nie może przekraczać 600 minut.";
        }

        return "";
    }

    // walidacja formatu daty i godziny seansu
    public string SprawdzDateSeansu(string dataGodzina)
    {
        if (string.IsNullOrWhiteSpace(dataGodzina))
        {
            return "Data i godzina seansu nie mogą być puste.";
        }

        if (!DateTime.TryParse(dataGodzina, out _))
        {
            return $"Niepoprawny format daty: '{dataGodzina}'. Oczekiwany format: YYYY-MM-DD HH:MM";
        }

        return "";
    }

    // walidacja numeru sali
    public string SprawdzSale(int sala)
    {
        if (sala < 1)
        {
            return "Numer sali musi być większy od zera.";
        }

        if (sala > 20)
        {
            return "Numer sali nie może przekraczać 20.";
        }

        return "";
    }
}
