using System_rezerwacji_miejsc.db;

namespace System_rezerwacji_miejsc.Logika;

// jedna pozycja w koszyku
// laczy konkretne miejsce z konkretnym seansem
public class PozycjaKoszyka
{
    public Miejsce Miejsce { get; }
    public Seans Seans { get; }

    public PozycjaKoszyka(Miejsce miejsce, Seans seans)
    {
        Miejsce = miejsce ?? throw new ArgumentNullException(nameof(miejsce));
        Seans = seans ?? throw new ArgumentNullException(nameof(seans));
    }

    // opis pokazywany w listBox w Form1
    public string Opis() => $"[{Seans.DataGodzina} Sala {Seans.Sala}] {Miejsce}";

    // porownywanie pozycji potrzebne dla Contains i Remove
    public override bool Equals(object? obj) =>
        obj is PozycjaKoszyka p && p.Miejsce.Id == Miejsce.Id && p.Seans.Id == Seans.Id;

    public override int GetHashCode() => HashCode.Combine(Miejsce.Id, Seans.Id);
}

// koszyk rezerwacji
// przechowuje wybrane miejsca do momentu zatwierdzenia
// nie zalezy od UI ani od bazy danych
public class Koszyk
{
    // wewnetrzna lista pozycji
    private readonly List<PozycjaKoszyka> pozycje = new();

    // dodaje miejsce do koszyka
    // jesli juz jest to nie robi nic (idempotentnosc)
    public void Dodaj(Miejsce miejsce, Seans seans)
    {
        var pozycja = new PozycjaKoszyka(miejsce, seans);
        if (!pozycje.Contains(pozycja))
        {
            pozycje.Add(pozycja);
        }
    }

    // usuwa pojedyncza pozycje z koszyka
    public void Usun(Miejsce miejsce, Seans seans)
    {
        var pozycja = new PozycjaKoszyka(miejsce, seans);
        pozycje.Remove(pozycja);
    }

    // sprawdza czy miejsce juz jest w koszyku
    public bool Zawiera(Miejsce miejsce, Seans seans)
    {
        var pozycja = new PozycjaKoszyka(miejsce, seans);
        return pozycje.Contains(pozycja);
    }

    // szuka pozycje po opisie z listBox
    public PozycjaKoszyka? ZnajdzPoOpisie(string opis)
    {
        return pozycje.FirstOrDefault(p => p.Opis() == opis);
    }

    // czysci caly koszyk - anulowanie wszystkich rezerwacji
    public void Wyczysc()
    {
        pozycje.Clear();
    }

    // liczba pozycji w koszyku
    public int Policz() => pozycje.Count;

    // zwraca kopie wszystkich pozycji
    public IReadOnlyList<PozycjaKoszyka> PobierzWszystkie() => pozycje.ToList();

    // zwraca pozycje dla konkretnego seansu
    public IReadOnlyList<PozycjaKoszyka> PobierzDlaSeansu(int seansId) =>
        pozycje.Where(p => p.Seans.Id == seansId).ToList();
}
