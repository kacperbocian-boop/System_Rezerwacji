using System_rezerwacji_miejsc.db;

namespace System_rezerwacji_miejsc.Logika;

// serwis koordynujacy proces rezerwacji
// laczy koszyk z baza danych
// operuje na obiektach domenowych nie na UI
public class RezerwacjaService
{
    // writer do zapisu rezerwacji w bazie
    private readonly DatabaseWriter zapis;

    public RezerwacjaService(DatabaseWriter zapis)
    {
        this.zapis = zapis ?? throw new ArgumentNullException(nameof(zapis));
    }

    // zapisuje wszystkie pozycje z koszyka do bazy
    // zwraca liczbe pomyslnie zarezerwowanych miejsc
    public int Zarezerwuj(Koszyk koszyk)
    {
        if (koszyk is null)
        {
            throw new ArgumentNullException(nameof(koszyk));
        }

        if (koszyk.Policz() == 0)
        {
            return 0;
        }

        // zapisujemy kazda pozycje osobno
        int licznik = 0;
        foreach (var pozycja in koszyk.PobierzWszystkie())
        {
            zapis.DodajRezerwacje(pozycja.Seans.Id, pozycja.Miejsce.Id);
            licznik++;
        }

        return licznik;
    }

    // anuluje pojedyncza rezerwacje
    // zwraca true jesli rezerwacja istniala i zostala usunieta
    public bool AnulujRezerwacje(DatabaseReader czytnik, int seansId, int miejsceId)
    {
        var zajete = czytnik.PobierzZarezerwowaneMiejsca(seansId);
        if (!zajete.Contains(miejsceId))
        {
            return false;
        }

        zapis.UsunRezerwacje(seansId, miejsceId);
        return true;
    }

    // anuluje wszystkie rezerwacje na dany seans
    // zwraca liczbe usunietych rezerwacji
    public int AnulujWszystkieRezerwacje(DatabaseReader czytnik, int seansId)
    {
        var zajete = czytnik.PobierzZarezerwowaneMiejsca(seansId);
        if (zajete.Count == 0)
        {
            return 0;
        }

        zapis.UsunWszystkieRezerwacje(seansId);
        return zajete.Count;
    }
}
