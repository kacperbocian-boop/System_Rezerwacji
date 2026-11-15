using System_rezerwacji_miejsc.db;
using Xunit;

namespace Testy;

// Testy do sprawdzania czy baza danych w ogole dziala
public class DatabaseTesty
{
    [Fact]
    public void UtworzBaze_CzyNieWyrzucaBledow()
    {
        var baza = new Database();
        baza.UtworzBaze();
        // jak przejdzie bez błędu to znaczy ze dziala
    }

    [Fact]
    public void PobierzWszystkieFilmy_CzyZwracaDane()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var filmy = reader.PobierzWszystkieFilmy();
        
        // sprawdzamy czy lista nie jest pusta
        Assert.NotEmpty(filmy);
    }

    [Fact]
    public void PobierzWszystkieFilmy_CzyZaladowaloIncepcje()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var filmy = reader.PobierzWszystkieFilmy();
        
        Assert.Contains(filmy, f => f.Tytul == "Incepcja");
    }

    // sprawdzamy czy generuja sie fotele dla sali (4 rzedy po 8 miejsc)
    [Fact]
    public void PobierzMiejsca_DlaSali_CzyZwracaRowno32Miejsca()
    {
        var baza = new Database();
        baza.UtworzBaze();
        var reader = new DatabaseReader();
        var miejsca = reader.PobierzObiektyMiejsc(1);
        
        Assert.Equal(32, miejsca.Count);
    }
}