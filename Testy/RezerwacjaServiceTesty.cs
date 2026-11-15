using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;
using Xunit;

namespace Testy;

public class RezerwacjaServiceTesty
{
    // zabezpieczenie zeby apka sie nie wywalila jak nie ma koszyka
    [Fact]
    public void Zarezerwuj_KiedyBrakKoszyka_PowinnoWyrzucicBlad()
    {
        var writer = new DatabaseWriter();
        var service = new RezerwacjaService(writer);
        Assert.Throws<ArgumentNullException>(() => service.Zarezerwuj(null!));
    }

    [Fact]
    public void Zarezerwuj_KiedyKoszykJestPusty_NieZapisujeNic()
    {
        var writer = new DatabaseWriter();
        var service = new RezerwacjaService(writer);
        var koszyk = new Koszyk();
        
        int liczbaZapisanych = service.Zarezerwuj(koszyk);
        Assert.Equal(0, liczbaZapisanych);
    }
}