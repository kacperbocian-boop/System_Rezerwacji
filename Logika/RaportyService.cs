using System_rezerwacji_miejsc.db;

namespace System_rezerwacji_miejsc.Logika;

// statystyki pojedynczego filmu
public class StatystykaFilmu
{
    public string Tytul { get; set; } = "";
    public int LiczbaRezerwacji { get; set; }
}

// oblozenie pojedynczego seansu
public class OblozenieSeansu
{
    public int SeansId { get; set; }
    public string FilmTytul { get; set; } = "";
    public string DataGodzina { get; set; } = "";
    public int Sala { get; set; }
    public int LiczbaZajetych { get; set; }
    public int LiczbaWszystkich { get; set; }

    // procent oblozenia liczony automatycznie
    public double ProcentOblozenia => LiczbaWszystkich == 0
        ? 0
        : Math.Round((double)LiczbaZajetych / LiczbaWszystkich * 100, 1);

    // opis do listBox w formularzu raportow
    public override string ToString() =>
        $"{FilmTytul} | {DataGodzina} | Sala {Sala} | {LiczbaZajetych}/{LiczbaWszystkich} ({ProcentOblozenia}%)";
}

// pojedyncza rezerwacja do raportu
public class WpisRezerwacji
{
    public int RezerwacjaId { get; set; }
    public string FilmTytul { get; set; } = "";
    public string DataGodzina { get; set; } = "";
    public int Sala { get; set; }
    public int Rzad { get; set; }
    public int NumerMiejsca { get; set; }

    public override string ToString() =>
        $"#{RezerwacjaId} | {FilmTytul} | {DataGodzina} | Sala {Sala} | Rząd {Rzad}, Miejsce {NumerMiejsca}";
}

// serwis generujacy raporty i statystyki z bazy
// wykorzystywany przez FormRaporty w UI
public class RaportyService
{
    // czytnik do pobierania danych z bazy
    private readonly DatabaseReader czytnik = new();

    // pobiera wszystkie rezerwacje jako czytelne wpisy do raportu
    public List<WpisRezerwacji> PobierzWszystkieRezerwacje()
    {
        var wynik = new List<WpisRezerwacji>();

        // dla kazdego filmu pobieramy jego seanse
        var filmy = czytnik.PobierzWszystkieFilmy();
        foreach (var film in filmy)
        {
            var seanse = czytnik.PobierzSeanseDlaFilmu(film.Id);
            foreach (var seans in seanse)
            {
                var zajete = czytnik.PobierzZarezerwowaneMiejsca(seans.Id);
                foreach (var miejsceId in zajete)
                {
                    // szukamy obiekt miejsca po id w sali seansu
                    var miejscaSali = czytnik.PobierzObiektyMiejsc(seans.Sala);
                    var m = miejscaSali.FirstOrDefault(x => x.Id == miejsceId);
                    if (m is null) continue;

                    wynik.Add(new WpisRezerwacji
                    {
                        RezerwacjaId = wynik.Count + 1,
                        FilmTytul = film.Tytul,
                        DataGodzina = seans.DataGodzina,
                        Sala = seans.Sala,
                        Rzad = m.Rzad,
                        NumerMiejsca = m.NumerMiejsca
                    });
                }
            }
        }

        return wynik;
    }

    // liczba wszystkich rezerwacji w bazie
    public int PoliczRezerwacje() => PobierzWszystkieRezerwacje().Count;

    // liczba filmow w bazie
    public int PoliczFilmy() => czytnik.PobierzWszystkieFilmy().Count;

    // liczba wszystkich seansow
    public int PoliczSeanse()
    {
        int suma = 0;
        foreach (var film in czytnik.PobierzWszystkieFilmy())
        {
            suma += czytnik.PobierzSeanseDlaFilmu(film.Id).Count;
        }
        return suma;
    }

    // zwraca film z najwieksza liczba rezerwacji
    public StatystykaFilmu? NajpopularniejszyFilm()
    {
        var filmy = czytnik.PobierzWszystkieFilmy();
        StatystykaFilmu? top = null;

        foreach (var film in filmy)
        {
            int licznik = 0;
            var seanse = czytnik.PobierzSeanseDlaFilmu(film.Id);
            foreach (var seans in seanse)
            {
                licznik += czytnik.PobierzZarezerwowaneMiejsca(seans.Id).Count;
            }

            // aktualizujemy top jesli ten film ma wiecej rezerwacji
            if (top is null || licznik > top.LiczbaRezerwacji)
            {
                top = new StatystykaFilmu { Tytul = film.Tytul, LiczbaRezerwacji = licznik };
            }
        }

        return top;
    }

    // zwraca oblozenie wszystkich seansow
    public List<OblozenieSeansu> OblozenieSeanse()
    {
        var wynik = new List<OblozenieSeansu>();

        foreach (var film in czytnik.PobierzWszystkieFilmy())
        {
            var seanse = czytnik.PobierzSeanseDlaFilmu(film.Id);
            foreach (var seans in seanse)
            {
                int zajete = czytnik.PobierzZarezerwowaneMiejsca(seans.Id).Count;
                int wszystkie = czytnik.PobierzObiektyMiejsc(seans.Sala).Count;

                wynik.Add(new OblozenieSeansu
                {
                    SeansId = seans.Id,
                    FilmTytul = film.Tytul,
                    DataGodzina = seans.DataGodzina,
                    Sala = seans.Sala,
                    LiczbaZajetych = zajete,
                    LiczbaWszystkich = wszystkie
                });
            }
        }

        return wynik;
    }
}
