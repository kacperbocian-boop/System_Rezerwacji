using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;
using Xunit;

namespace Testy;

// testy walidacji danych wejsciowych
public class WalidacjaTesty
{
    private readonly Walidacja w = new();

    // ----- SprawdzKoszyk -----

    [Fact]
    public void SprawdzKoszyk_NullSeans_ZwracaBlad()
    {
        var koszyk = new Koszyk();
        var blad = w.SprawdzKoszyk(koszyk, null!);
        Assert.Contains("seans", blad.ToLower());
    }

    [Fact]
    public void SprawdzKoszyk_PustyKoszyk_ZwracaBlad()
    {
        var koszyk = new Koszyk();
        var seans = new Seans { Id = 1, FilmId = 1, DataGodzina = "2026-11-05 17:00", Sala = 1 };
        var blad = w.SprawdzKoszyk(koszyk, seans);
        Assert.Contains("pusty", blad.ToLower());
    }

    [Fact]
    public void SprawdzKoszyk_PoprawnyKoszyk_ZwracaPustyString()
    {
        var koszyk = new Koszyk();
        var seans = new Seans { Id = 1, FilmId = 1, DataGodzina = "2026-11-05 17:00", Sala = 1 };
        var miejsce = new Miejsce { Id = 1, Sala = 1, Rzad = 1, NumerMiejsca = 1 };
        koszyk.Dodaj(miejsce, seans);

        var blad = w.SprawdzKoszyk(koszyk, seans);
        Assert.Equal("", blad);
    }

    // ----- SprawdzFilm -----

    [Fact]
    public void SprawdzFilm_PustyTytul_ZwracaBlad()
    {
        var blad = w.SprawdzFilm("", 120);
        Assert.NotEqual("", blad);
    }

    [Fact]
    public void SprawdzFilm_DlugiTytul_ZwracaBlad()
    {
        var dlugiTytul = new string('a', 201);
        var blad = w.SprawdzFilm(dlugiTytul, 120);
        Assert.Contains("długi", blad.ToLower());
    }

    [Fact]
    public void SprawdzFilm_ZeroCzas_ZwracaBlad()
    {
        var blad = w.SprawdzFilm("Matrix", 0);
        Assert.Contains("większy od zera", blad.ToLower());
    }

    [Fact]
    public void SprawdzFilm_ZaDlugiCzas_ZwracaBlad()
    {
        var blad = w.SprawdzFilm("Matrix", 601);
        Assert.Contains("600", blad);
    }

    [Fact]
    public void SprawdzFilm_PoprawnyFilm_ZwracaPustyString()
    {
        var blad = w.SprawdzFilm("Incepcja", 148);
        Assert.Equal("", blad);
    }

    // ----- SprawdzDateSeansu -----

    [Fact]
    public void SprawdzDateSeansu_PustaData_ZwracaBlad()
    {
        var blad = w.SprawdzDateSeansu("");
        Assert.NotEqual("", blad);
    }

    [Fact]
    public void SprawdzDateSeansu_NiepoprawnyFormat_ZwracaBlad()
    {
        var blad = w.SprawdzDateSeansu("nie-poprawna-data");
        Assert.Contains("format", blad.ToLower());
    }

    [Fact]
    public void SprawdzDateSeansu_PoprawnaData_ZwracaPustyString()
    {
        var blad = w.SprawdzDateSeansu("2026-11-05 17:00");
        Assert.Equal("", blad);
    }

    // ----- SprawdzSale -----

    [Fact]
    public void SprawdzSale_Zero_ZwracaBlad()
    {
        var blad = w.SprawdzSale(0);
        Assert.NotEqual("", blad);
    }

    [Fact]
    public void SprawdzSale_Ponad20_ZwracaBlad()
    {
        var blad = w.SprawdzSale(21);
        Assert.Contains("20", blad);
    }

    [Fact]
    public void SprawdzSale_PoprawnaSala_ZwracaPustyString()
    {
        var blad = w.SprawdzSale(5);
        Assert.Equal("", blad);
    }
}
