using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;
using Xunit;

namespace Testy;

// Testy sprawdzajace czy dzialaja blokady i ostrzezenia dla uzytkownika
public class WalidacjaTesty
{
    private readonly Walidacja w = new();

    [Fact]
    public void SprawdzKoszyk_GdyPusty_WyswietlaBlad()
    {
        var koszyk = new Koszyk();
        var seans = new Seans { Id = 1, FilmId = 1, DataGodzina = "2026-11-05 17:00", Sala = 1 };
        
        var blad = w.SprawdzKoszyk(koszyk, seans);
        Assert.Contains("pusty", blad.ToLower()); // oczekujemy ze tresc bledu zawiera slowo pusty
    }

    [Fact]
    public void SprawdzFilm_BrakTytulu_ZwracaOstrzezenie()
    {
        var blad = w.SprawdzFilm("", 120);
        Assert.NotEqual("", blad); // blad nie moze byc pusty, cos musi wyskoczyc
    }

    [Fact]
    public void SprawdzFilm_CzasUjemnyLubZero_ZwracaOstrzezenie()
    {
        var blad = w.SprawdzFilm("Jakis Film", 0);
        Assert.Contains("większy od zera", blad.ToLower());
    }

    [Fact]
    public void SprawdzSale_NiepoprawnyNumerSali_ZwracaOstrzezenie()
    {
        var blad = w.SprawdzSale(99);
        Assert.Contains("20", blad); // u nas max to 20 sal wiec musi to zablokowac
    }
}