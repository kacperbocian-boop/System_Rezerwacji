using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;
using Xunit;

namespace Testy;

// testy serwisu rezerwacji
// integracja Koszyk + RezerwacjaService
public class RezerwacjaServiceTesty
{
    [Fact]
    public void Zarezerwuj_NullKoszyk_RzucaWyjatek()
    {
        var writer = new DatabaseWriter();
        var service = new RezerwacjaService(writer);
        Assert.Throws<ArgumentNullException>(() => service.Zarezerwuj(null!));
    }

    [Fact]
    public void Zarezerwuj_PustyKoszyk_ZwracaZero()
    {
        var writer = new DatabaseWriter();
        var service = new RezerwacjaService(writer);
        var koszyk = new Koszyk();
        int wynik = service.Zarezerwuj(koszyk);
        Assert.Equal(0, wynik);
    }
}
