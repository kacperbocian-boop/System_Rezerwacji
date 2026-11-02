using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;
using Xunit;

namespace Testy;

// testy klasy Koszyk
public class KoszykTesty
{
    // helper do tworzenia miejsca w testach
    private static Miejsce Miejsce(int id, int numer) => new()
    {
        Id = id,
        Sala = 1,
        Rzad = (numer - 1) / 8 + 1,
        NumerMiejsca = numer
    };

    // helper do tworzenia seansu w testach
    private static Seans Seans(int id = 1, int sala = 1) => new()
    {
        Id = id,
        FilmId = 1,
        DataGodzina = "2026-11-05 17:00",
        Sala = sala
    };

    [Fact]
    public void NowyKoszyk_JestPusty()
    {
        var k = new Koszyk();
        Assert.Equal(0, k.Policz());
    }

    [Fact]
    public void Dodaj_RosnieLicznik()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans());
        k.Dodaj(Miejsce(2, 2), Seans());
        Assert.Equal(2, k.Policz());
    }

    // dodanie tego samego miejsca nie powinno duplikowac
    [Fact]
    public void Dodaj_TenSamaPozycja_NieDuplikuje()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans());
        k.Dodaj(Miejsce(1, 1), Seans());
        Assert.Equal(1, k.Policz());
    }

    [Fact]
    public void Zawiera_ZwracaTrueDlaDodanejPozycji()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans());
        Assert.True(k.Zawiera(Miejsce(1, 1), Seans()));
    }

    [Fact]
    public void Zawiera_ZwracaFalseDlaNieDodanejPozycji()
    {
        var k = new Koszyk();
        Assert.False(k.Zawiera(Miejsce(99, 99), Seans()));
    }

    [Fact]
    public void Usun_ZmniejszaLicznik()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans());
        k.Dodaj(Miejsce(2, 2), Seans());
        k.Usun(Miejsce(1, 1), Seans());
        Assert.Equal(1, k.Policz());
    }

    [Fact]
    public void Wyczysc_OpróżniaKoszyk()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans());
        k.Dodaj(Miejsce(2, 2), Seans());
        k.Wyczysc();
        Assert.Equal(0, k.Policz());
    }

    [Fact]
    public void PobierzDlaSeansu_FiltrujePoIdSeansu()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans(id: 1));
        k.Dodaj(Miejsce(2, 2), Seans(id: 2));
        var wynik = k.PobierzDlaSeansu(1);
        Assert.Single(wynik);
    }

    [Fact]
    public void ZnajdzPoOpisie_ZwracaPozycjeDlaPoprawnegoOpisu()
    {
        var k = new Koszyk();
        k.Dodaj(Miejsce(1, 1), Seans());
        var pozycja = k.ZnajdzPoOpisie("[2026-11-05 17:00 Sala 1] Rząd 1, Miejsce 1");
        Assert.NotNull(pozycja);
    }

    [Fact]
    public void ZnajdzPoOpisie_ZwracaNullDlaNieistniejacegoOpisu()
    {
        var k = new Koszyk();
        var pozycja = k.ZnajdzPoOpisie("nieistniejący opis");
        Assert.Null(pozycja);
    }
}
