namespace System_rezerwacji_miejsc;

partial class FormRaporty
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.lblTytul = new System.Windows.Forms.Label();
        this.lblNaglowekLista = new System.Windows.Forms.Label();
        this.lstRezerwacje = new System.Windows.Forms.ListBox();
        this.lblStatystyki = new System.Windows.Forms.Label();
        this.lblLiczbaRezerwacji = new System.Windows.Forms.Label();
        this.lblLiczbaFilmow = new System.Windows.Forms.Label();
        this.lblLiczbaSeansow = new System.Windows.Forms.Label();
        this.lblTopFilm = new System.Windows.Forms.Label();
        this.lblNaglowekOblozenie = new System.Windows.Forms.Label();
        this.lstOblozenie = new System.Windows.Forms.ListBox();
        this.btnOdswiez = new System.Windows.Forms.Button();
        this.btnZamknij = new System.Windows.Forms.Button();
        this.SuspendLayout();
        //
        // lblTytul
        //
        this.lblTytul.AutoSize = true;
        this.lblTytul.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.lblTytul.ForeColor = System.Drawing.Color.White;
        this.lblTytul.Location = new System.Drawing.Point(20, 12);
        this.lblTytul.Name = "lblTytul";
        this.lblTytul.Size = new System.Drawing.Size(220, 25);
        this.lblTytul.Text = "Raporty i statystyki";
        //
        // lblNaglowekLista
        //
        this.lblNaglowekLista.AutoSize = true;
        this.lblNaglowekLista.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblNaglowekLista.ForeColor = System.Drawing.Color.White;
        this.lblNaglowekLista.Location = new System.Drawing.Point(20, 55);
        this.lblNaglowekLista.Name = "lblNaglowekLista";
        this.lblNaglowekLista.Text = "Wszystkie rezerwacje:";
        //
        // lstRezerwacje
        //
        this.lstRezerwacje.BackColor = System.Drawing.Color.FromArgb(42, 44, 57);
        this.lstRezerwacje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.lstRezerwacje.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lstRezerwacje.ForeColor = System.Drawing.Color.White;
        this.lstRezerwacje.FormattingEnabled = true;
        this.lstRezerwacje.ItemHeight = 15;
        this.lstRezerwacje.Location = new System.Drawing.Point(20, 78);
        this.lstRezerwacje.Name = "lstRezerwacje";
        this.lstRezerwacje.Size = new System.Drawing.Size(420, 250);
        //
        // lblStatystyki
        //
        this.lblStatystyki.AutoSize = true;
        this.lblStatystyki.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblStatystyki.ForeColor = System.Drawing.Color.White;
        this.lblStatystyki.Location = new System.Drawing.Point(460, 55);
        this.lblStatystyki.Name = "lblStatystyki";
        this.lblStatystyki.Text = "Statystyki ogólne:";
        //
        // lblLiczbaRezerwacji
        //
        this.lblLiczbaRezerwacji.AutoSize = true;
        this.lblLiczbaRezerwacji.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblLiczbaRezerwacji.ForeColor = System.Drawing.Color.FromArgb(100, 181, 246);
        this.lblLiczbaRezerwacji.Location = new System.Drawing.Point(460, 82);
        this.lblLiczbaRezerwacji.Name = "lblLiczbaRezerwacji";
        this.lblLiczbaRezerwacji.Text = "Liczba rezerwacji: 0";
        //
        // lblLiczbaFilmow
        //
        this.lblLiczbaFilmow.AutoSize = true;
        this.lblLiczbaFilmow.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblLiczbaFilmow.ForeColor = System.Drawing.Color.FromArgb(100, 181, 246);
        this.lblLiczbaFilmow.Location = new System.Drawing.Point(460, 105);
        this.lblLiczbaFilmow.Name = "lblLiczbaFilmow";
        this.lblLiczbaFilmow.Text = "Liczba filmów w bazie: 0";
        //
        // lblLiczbaSeansow
        //
        this.lblLiczbaSeansow.AutoSize = true;
        this.lblLiczbaSeansow.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblLiczbaSeansow.ForeColor = System.Drawing.Color.FromArgb(100, 181, 246);
        this.lblLiczbaSeansow.Location = new System.Drawing.Point(460, 128);
        this.lblLiczbaSeansow.Name = "lblLiczbaSeansow";
        this.lblLiczbaSeansow.Text = "Liczba seansów: 0";
        //
        // lblTopFilm
        //
        this.lblTopFilm.AutoSize = true;
        this.lblTopFilm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.lblTopFilm.ForeColor = System.Drawing.Color.FromArgb(255, 213, 79);
        this.lblTopFilm.Location = new System.Drawing.Point(460, 151);
        this.lblTopFilm.Name = "lblTopFilm";
        this.lblTopFilm.Text = "Najpopularniejszy film: -";
        //
        // lblNaglowekOblozenie
        //
        this.lblNaglowekOblozenie.AutoSize = true;
        this.lblNaglowekOblozenie.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblNaglowekOblozenie.ForeColor = System.Drawing.Color.White;
        this.lblNaglowekOblozenie.Location = new System.Drawing.Point(460, 185);
        this.lblNaglowekOblozenie.Name = "lblNaglowekOblozenie";
        this.lblNaglowekOblozenie.Text = "Obłożenie seansów:";
        //
        // lstOblozenie
        //
        this.lstOblozenie.BackColor = System.Drawing.Color.FromArgb(42, 44, 57);
        this.lstOblozenie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.lstOblozenie.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lstOblozenie.ForeColor = System.Drawing.Color.White;
        this.lstOblozenie.FormattingEnabled = true;
        this.lstOblozenie.ItemHeight = 15;
        this.lstOblozenie.Location = new System.Drawing.Point(460, 208);
        this.lstOblozenie.Name = "lstOblozenie";
        this.lstOblozenie.Size = new System.Drawing.Size(290, 120);
        //
        // btnOdswiez
        //
        this.btnOdswiez.BackColor = System.Drawing.Color.FromArgb(70, 74, 96);
        this.btnOdswiez.FlatAppearance.BorderSize = 0;
        this.btnOdswiez.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnOdswiez.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnOdswiez.ForeColor = System.Drawing.Color.White;
        this.btnOdswiez.Location = new System.Drawing.Point(460, 340);
        this.btnOdswiez.Name = "btnOdswiez";
        this.btnOdswiez.Size = new System.Drawing.Size(140, 35);
        this.btnOdswiez.Text = "Odśwież";
        this.btnOdswiez.UseVisualStyleBackColor = false;
        this.btnOdswiez.Click += new System.EventHandler(this.btnOdswiez_Click);
        //
        // btnZamknij
        //
        this.btnZamknij.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
        this.btnZamknij.FlatAppearance.BorderSize = 0;
        this.btnZamknij.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnZamknij.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnZamknij.ForeColor = System.Drawing.Color.White;
        this.btnZamknij.Location = new System.Drawing.Point(610, 340);
        this.btnZamknij.Name = "btnZamknij";
        this.btnZamknij.Size = new System.Drawing.Size(140, 35);
        this.btnZamknij.Text = "Zamknij";
        this.btnZamknij.UseVisualStyleBackColor = false;
        this.btnZamknij.Click += new System.EventHandler(this.btnZamknij_Click);
        //
        // FormRaporty
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(30, 31, 41);
        this.ClientSize = new System.Drawing.Size(770, 400);
        this.Controls.Add(this.lblTytul);
        this.Controls.Add(this.lblNaglowekLista);
        this.Controls.Add(this.lstRezerwacje);
        this.Controls.Add(this.lblStatystyki);
        this.Controls.Add(this.lblLiczbaRezerwacji);
        this.Controls.Add(this.lblLiczbaFilmow);
        this.Controls.Add(this.lblLiczbaSeansow);
        this.Controls.Add(this.lblTopFilm);
        this.Controls.Add(this.lblNaglowekOblozenie);
        this.Controls.Add(this.lstOblozenie);
        this.Controls.Add(this.btnOdswiez);
        this.Controls.Add(this.btnZamknij);
        this.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "FormRaporty";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Raporty i statystyki - System Rezerwacji";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblTytul;
    private System.Windows.Forms.Label lblNaglowekLista;
    private System.Windows.Forms.ListBox lstRezerwacje;
    private System.Windows.Forms.Label lblStatystyki;
    private System.Windows.Forms.Label lblLiczbaRezerwacji;
    private System.Windows.Forms.Label lblLiczbaFilmow;
    private System.Windows.Forms.Label lblLiczbaSeansow;
    private System.Windows.Forms.Label lblTopFilm;
    private System.Windows.Forms.Label lblNaglowekOblozenie;
    private System.Windows.Forms.ListBox lstOblozenie;
    private System.Windows.Forms.Button btnOdswiez;
    private System.Windows.Forms.Button btnZamknij;
}
