using System_rezerwacji_miejsc.db;
using Xunit;

namespace Testy;

// testy warstwy dostepu do danych
// DatabaseReader i DatabaseWriter
// korzystaja z prawdziwej bazy SQLite tworzona automatycznie
public class DatabaseTesty
{
    [Fact]
    public void UtworzBaze_TworzyBazeBezBledow()
    {
        var baza = new Database();
        baza.UtworzBaze();
        // brak wyjatku = sukces
    }

    [Fact]
    public void PobierzWszystkieFilmy_ZwracaPrzykladoweFilmy()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var filmy = reader.PobierzWszystkieFilmy();
        Assert.NotEmpty(filmy);
    }

    [Fact]
    public void PobierzWszystkieFilmy_ZawieraIncepcje()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var filmy = reader.PobierzWszystkieFilmy();
        Assert.Contains(filmy, f => f.Tytul == "Incepcja");
    }

    [Fact]
    public void PobierzSeanseDlaFilmu_ZwracaSeanseDlaPierwszegoFilmu()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var filmy = reader.PobierzWszystkieFilmy();
        var seanse = reader.PobierzSeanseDlaFilmu(filmy[0].Id);
        Assert.NotEmpty(seanse);
    }

    // sala 1 ma 32 miejsca (4 rzeddy po 8)
    [Fact]
    public void PobierzObiektyMiejsc_DlaSali1_Zwraca32Miejsca()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var miejsca = reader.PobierzObiektyMiejsc(1);
        Assert.Equal(32, miejsca.Count);
    }

    // dla seansu 1 sa przykladowe rezerwacje na miejsca 10 11 12
    [Fact]
    public void PobierzZarezerwowaneMiejsca_DlaSeansu1_ZawieraMiejsca10_11_12()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var zajete = reader.PobierzZarezerwowaneMiejsca(1);
        Assert.Contains(10, zajete);
        Assert.Contains(11, zajete);
        Assert.Contains(12, zajete);
    }
}
