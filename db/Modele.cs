namespace System_rezerwacji_miejsc.db;

// model filmu w bazie
public class Film
{
    public int Id { get; set; }
    public string Tytul { get; set; } = "";
    public string Opis { get; set; } = "";
    public int CzasTrwania { get; set; }
    public string ImageURL { get; set; } = "";

    // w combo box pokazujemy tylko tytul
    public override string ToString() => Tytul;
}

// model seansu
// jeden film moze miec wiele seansow
public class Seans
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public string DataGodzina { get; set; } = "";
    public int Sala { get; set; }

    // opis w combo
    public override string ToString() => $"{DataGodzina} (Sala {Sala})";
}

// model miejsca na sali
public class Miejsce
{
    public int Id { get; set; }
    public int Sala { get; set; }
    public int Rzad { get; set; }
    public int NumerMiejsca { get; set; }

    // opis w listBox koszyka
    public override string ToString() => $"Rząd {Rzad}, Miejsce {NumerMiejsca}";
}
