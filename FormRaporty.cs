using System;
using System.Drawing;
using System.Windows.Forms;
using System_rezerwacji_miejsc.Logika;

namespace System_rezerwacji_miejsc;

// okno raportow i statystyk
// otwierane z glownego formularza przyciskiem RAPORTY
public partial class FormRaporty : Form
{
    // serwis statystyk - wczytuje dane z bazy
    private readonly RaportyService raporty = new();

    public FormRaporty()
    {
        InitializeComponent();
        WczytajRaporty();
    }

    // laduje wszystkie dane do kontrolek
    private void WczytajRaporty()
    {
        // 1 lista wszystkich rezerwacji
        lstRezerwacje.Items.Clear();
        var rezerwacje = raporty.PobierzWszystkieRezerwacje();
        foreach (var r in rezerwacje)
        {
            lstRezerwacje.Items.Add(r);
        }

        // 2 statystyki ogolne
        lblLiczbaRezerwacji.Text = $"Liczba rezerwacji: {raporty.PoliczRezerwacje()}";
        lblLiczbaFilmow.Text = $"Liczba filmów w bazie: {raporty.PoliczFilmy()}";
        lblLiczbaSeansow.Text = $"Liczba seansów: {raporty.PoliczSeanse()}";

        // 3 najpopularniejszy film wg liczby rezerwacji
        var top = raporty.NajpopularniejszyFilm();
        lblTopFilm.Text = top is null
            ? "Najpopularniejszy film: -"
            : $"Najpopularniejszy film: {top.Tytul} ({top.LiczbaRezerwacji} rezerwacji)";

        // 4 oblozenie seansow
        lstOblozenie.Items.Clear();
        var oblozenie = raporty.OblozenieSeanse();
        foreach (var o in oblozenie)
        {
            lstOblozenie.Items.Add(o);
        }
    }

    // zamkniecie okna
    private void btnZamknij_Click(object? sender, EventArgs e)
    {
        Close();
    }

    // odswiezenie danych
    private void btnOdswiez_Click(object? sender, EventArgs e)
    {
        WczytajRaporty();
    }
}
