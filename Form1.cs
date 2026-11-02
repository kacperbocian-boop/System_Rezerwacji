using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System_rezerwacji_miejsc.db;
using System_rezerwacji_miejsc.Logika;

namespace System_rezerwacji_miejsc;

// glowny formularz aplikacji
// laczy 3 warstwy: UI, logike i dane
public partial class Form1 : Form
{
    // warstwa danych
    private readonly Database baza = new();
    private readonly DatabaseReader czytnik = new();
    private readonly DatabaseWriter zapis = new();

    // warstwa logiki
    private readonly Koszyk koszyk = new();
    private readonly RezerwacjaService rezerwacjaService;
    private readonly Walidacja walidacja = new();

    public Form1()
    {
        InitializeComponent();

        // serwis rezerwacji potrzebuje writera do zapisu
        rezerwacjaService = new RezerwacjaService(zapis);

        // lapanie bledow ladowania plakatu z netu
        pictureBox1.LoadCompleted += PictureBox1_LoadCompleted;

        // tworzymy baze i wczytujemy filmy
        baza.UtworzBaze();
        WczytajFilmy();
    }

    // pobiera filmy z bazy i laduje do combo
    private void WczytajFilmy()
    {
        listaFilmow.Items.Clear();

        List<Film> filmy = czytnik.PobierzWszystkieFilmy();
        foreach (Film film in filmy)
        {
            listaFilmow.Items.Add(film);
        }

        // zaznacz pierwszy film zeby seanse sie podciagnely
        if (listaFilmow.Items.Count > 0)
        {
            listaFilmow.SelectedIndex = 0;
        }
    }

    // wybranie filmu z listy
    private void listaFilmow_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (listaFilmow.SelectedItem is not Film wybranyFilm)
        {
            return;
        }

        // opis i czas trwania
        txtOpis.Text = wybranyFilm.Opis;
        lblCzasTrwania.Text = $"Czas trwania: {wybranyFilm.CzasTrwania} min";

        // ladujemy plakat
        ZaladujZdjecieFilmu(wybranyFilm.ImageURL, wybranyFilm.Tytul);

        // seanse tylko dla tego filmu
        listaSeansow.Items.Clear();
        List<Seans> seanse = czytnik.PobierzSeanseDlaFilmu(wybranyFilm.Id);
        foreach (Seans seans in seanse)
        {
            listaSeansow.Items.Add(seans);
        }

        if (listaSeansow.Items.Count > 0)
        {
            listaSeansow.SelectedIndex = 0;
        }
        else
        {
            // brak seansow - czyscimy sale
            panel1.Controls.Clear();
            koszyk.Wyczysc();
            AktualizujWyswietlanieKoszyka();
        }
    }

    // wybranie seansu z listy
    private void listaSeansow_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (listaSeansow.SelectedItem is not Seans wybranySeans)
        {
            return;
        }

        // czyscimy sale i koszyk przed przerysowaniem
        panel1.Controls.Clear();
        koszyk.Wyczysc();
        AktualizujWyswietlanieKoszyka();

        lblEkran.Text = $"────── EKRAN KINOWY (SALA {wybranySeans.Sala}) ──────";

        // wszystkie miejsca w tej sali
        List<Miejsce> miejsca = czytnik.PobierzObiektyMiejsc(wybranySeans.Sala);

        // ktore miejsca sa juz zajete
        List<int> zajeteId = czytnik.PobierzZarezerwowaneMiejsca(wybranySeans.Id);

        // uklad przyciskow na sali
        int marginesLewy = 15;
        int marginesGorny = 15;
        int rozmiar = 36;
        int odstep = 42;

        // tworzymy przycisk dla kazdego miejsca
        foreach (var miejsce in miejsca)
        {
            Button przycisk = new Button();
            przycisk.Size = new Size(rozmiar, rozmiar);
            przycisk.Text = miejsce.NumerMiejsca.ToString();
            przycisk.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            przycisk.FlatStyle = FlatStyle.Flat;
            przycisk.FlatAppearance.BorderSize = 1;
            // zapamietujemy miejsce w Tag zeby poznac ktore to kliknieto
            przycisk.Tag = miejsce;

            // 8 foteli w rzedzie
            int kolumna = (miejsce.NumerMiejsca - 1) % 8;
            int rzad = miejsce.Rzad - 1;

            przycisk.Location = new Point(
                marginesLewy + kolumna * odstep,
                marginesGorny + rzad * odstep
            );

            // kolorowanie zaleznie od statusu
            if (zajeteId.Contains(miejsce.Id))
            {
                // zajete - czerwone i zablokowane
                przycisk.BackColor = Color.FromArgb(198, 40, 40);
                przycisk.ForeColor = Color.White;
                przycisk.FlatAppearance.BorderColor = Color.FromArgb(183, 28, 28);
                przycisk.Enabled = false;
            }
            else
            {
                // wolne - zielone i klikalne
                przycisk.BackColor = Color.FromArgb(46, 125, 50);
                przycisk.ForeColor = Color.White;
                przycisk.FlatAppearance.BorderColor = Color.FromArgb(27, 94, 32);
                przycisk.Cursor = Cursors.Hand;
                przycisk.Click += Miejsce_Click;
            }

            panel1.Controls.Add(przycisk);
        }
    }

    // klikniecie w przycisk miejsca na sali
    private void Miejsce_Click(object? sender, EventArgs e)
    {
        if (sender is not Button przycisk || przycisk.Tag is not Miejsce miejsce)
        {
            return;
        }

        if (listaSeansow.SelectedItem is not Seans seans)
        {
            return;
        }

        // jesli juz w koszyku to usuwamy
        if (koszyk.Zawiera(miejsce, seans))
        {
            koszyk.Usun(miejsce, seans);
            przycisk.BackColor = Color.FromArgb(46, 125, 50); // zielony
        }
        else
        {
            // dodajemy do koszyka
            koszyk.Dodaj(miejsce, seans);
            przycisk.BackColor = Color.FromArgb(245, 158, 11); // zolty pomaranczowy
        }

        AktualizujWyswietlanieKoszyka();
    }

    // usuniecie pojedynczego miejsca z koszyka
    private void btnUsunMiejsce_Click(object? sender, EventArgs e)
    {
        if (listBox1.SelectedItem is not string opisWybranego)
        {
            return;
        }

        // szukamy pozycje po opisie bo listBox trzyma stringi
        var pozycja = koszyk.ZnajdzPoOpisie(opisWybranego);
        if (pozycja is null) return;

        koszyk.Usun(pozycja.Miejsce, pozycja.Seans);

        // przywroc zielony kolor na sali
        foreach (Control kontrolka in panel1.Controls)
        {
            if (kontrolka is Button btn && btn.Tag is Miejsce m && m.Id == pozycja.Miejsce.Id)
            {
                btn.BackColor = Color.FromArgb(46, 125, 50);
                break;
            }
        }

        AktualizujWyswietlanieKoszyka();
    }

    // czyszczenie calego koszyka
    private void btnWyczyscKoszyk_Click(object? sender, EventArgs e)
    {
        koszyk.Wyczysc();

        // wszystkie wolne miejsca wracaja do zielonego
        foreach (Control kontrolka in panel1.Controls)
        {
            if (kontrolka is Button btn && btn.Enabled)
            {
                btn.BackColor = Color.FromArgb(46, 125, 50);
            }
        }

        AktualizujWyswietlanieKoszyka();
    }

    // zatwierdzenie rezerwacji
    private void btnZarezerwuj_Click(object? sender, EventArgs e)
    {
        if (listaSeansow.SelectedItem is not Seans wybranySeans)
        {
            MessageBox.Show("Proszę najpierw wybrać seans.", "Brak seansu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // sprawdzamy koszyk przez walidacje
        string blad = walidacja.SprawdzKoszyk(koszyk, wybranySeans);
        if (!string.IsNullOrEmpty(blad))
        {
            MessageBox.Show(blad, "Nie można zarezerwować", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // zapisujemy przez serwis
        int liczba = rezerwacjaService.Zarezerwuj(koszyk);

        MessageBox.Show(
            $"Pomyślnie zarezerwowano {liczba} miejsc na seans:\n{wybranySeans.DataGodzina} (Sala {wybranySeans.Sala}).",
            "Rezerwacja potwierdzona",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );

        koszyk.Wyczysc();

        // odswiezamy sale zeby nowe rezerwacje byly czerwone
        listaSeansow_SelectedIndexChanged(sender, e);
    }

    // otwarcie okna raportow
    private void btnRaporty_Click(object? sender, EventArgs e)
    {
        using var form = new FormRaporty();
        form.ShowDialog(this);
    }

    // przerysowanie listBoxa z koszykiem
    private void AktualizujWyswietlanieKoszyka()
    {
        listBox1.Items.Clear();
        foreach (var p in koszyk.PobierzWszystkie())
        {
            listBox1.Items.Add(p.Opis());
        }
        lblPodsumowanie.Text = $"Wybrano miejsc: {koszyk.Policz()}";
    }

    // ladowanie plakatu z url lub fallback na rysowany
    private void ZaladujZdjecieFilmu(string url, string tytul)
    {
        // najpierw pozbywamy sie starego obrazka
        if (pictureBox1.Image != null)
        {
            var stary = pictureBox1.Image;
            pictureBox1.Image = null;
            stary.Dispose();
        }

        if (!string.IsNullOrWhiteSpace(url) && (url.StartsWith("http://") || url.StartsWith("https://")))
        {
            try
            {
                pictureBox1.LoadAsync(url);
                return;
            }
            catch
            {
                // jak sie nie uda to rysujemy zastepczy
            }
        }

        RysujZastepczyPlakat(tytul);
    }

    // jak ladowanie z netu sie nie uda to rysujemy placeholder
    private void PictureBox1_LoadCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error != null || e.Cancelled)
        {
            if (listaFilmow.SelectedItem is Film film)
            {
                RysujZastepczyPlakat(film.Tytul);
            }
        }
    }

    // rysuje szary plakat z napisem FILM i tytulem
    private void RysujZastepczyPlakat(string tytul)
    {
        Bitmap bmp = new Bitmap(235, 225);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.FromArgb(24, 25, 35));

            using Font fIkona = new Font("Segoe UI", 26);
            using Font fTytul = new Font("Segoe UI", 12, FontStyle.Bold);
            using Brush pBialy = new SolidBrush(Color.White);

            // napis FILM na srodku
            g.DrawString("FILM", fIkona, pBialy, new PointF(70, 35));

            // tytul na dole
            RectangleF rect = new RectangleF(15, 105, 205, 80);
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(tytul, fTytul, pBialy, rect, sf);
        }

        pictureBox1.Image = bmp;
    }
}
