using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;
using Xunit;

namespace Testy;

// Testy sprawdzajace dodawanie i usuwanie biletow
public class KoszykTesty
{
    // metoda pomocnicza zeby nie pisac tego samego
    private static Miejsce StworzMiejsce(int id) => new()
    {
        Id = id,
        Sala = 1,
        Rzad = 1,
        NumerMiejsca = id
    };

    private static Seans StworzSeans() => new()
    {
        Id = 1,
        FilmId = 1,
        DataGodzina = "2026-11-05 17:00",
        Sala = 1
    };

    [Fact]
    public void Koszyk_NaPoczatkuPowinienBycPusty()
    {
        var k = new Koszyk();
        Assert.Equal(0, k.Policz());
    }

    [Fact]
    public void Dodaj_CzyZwiekszaIloscMiejsc()
    {
        var k = new Koszyk();
        k.Dodaj(StworzMiejsce(1), StworzSeans());
        k.Dodaj(StworzMiejsce(2), StworzSeans());
        
        Assert.Equal(2, k.Policz());
    }

    // wazny test - co jak ktos kliknie dwa razy to samo
    [Fact]
    public void Dodaj_PodwojneKlikniecie_NiePowinnoZdublowacMiejsca()
    {
        var k = new Koszyk();
        k.Dodaj(StworzMiejsce(1), StworzSeans());
        k.Dodaj(StworzMiejsce(1), StworzSeans());
        
        Assert.Equal(1, k.Policz());
    }

    [Fact]
    public void Wyczysc_CzyPoprawnieZerujeKoszyk()
    {
        var k = new Koszyk();
        k.Dodaj(StworzMiejsce(1), StworzSeans());
        k.Dodaj(StworzMiejsce(2), StworzSeans());
        k.Wyczysc();
        
        Assert.Equal(0, k.Policz());
    }
}