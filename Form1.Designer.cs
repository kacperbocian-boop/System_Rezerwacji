namespace System_rezerwacji_miejsc;

partial class Form1
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.lblPodtytul = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.listaFilmow = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.listaSeansow = new System.Windows.Forms.ComboBox();
        this.lblCzasTrwania = new System.Windows.Forms.Label();
        this.txtOpis = new System.Windows.Forms.TextBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.lblNaglowekSali = new System.Windows.Forms.Label();
        this.lblEkran = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.panelLegenda = new System.Windows.Forms.Panel();
        this.lblLegendaZajete = new System.Windows.Forms.Label();
        this.boxZajete = new System.Windows.Forms.Panel();
        this.lblLegendaWybrane = new System.Windows.Forms.Label();
        this.boxWybrane = new System.Windows.Forms.Panel();
        this.lblLegendaWolne = new System.Windows.Forms.Label();
        this.boxWolne = new System.Windows.Forms.Panel();
        this.lblNaglowekKoszyka = new System.Windows.Forms.Label();
        this.listBox1 = new System.Windows.Forms.ListBox();
        this.lblPodsumowanie = new System.Windows.Forms.Label();
        this.btnUsunMiejsce = new System.Windows.Forms.Button();
        this.btnWyczyscKoszyk = new System.Windows.Forms.Button();
        this.btnZarezerwuj = new System.Windows.Forms.Button();
        this.btnRaporty = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.panelLegenda.SuspendLayout();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(25, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(370, 30);
        this.label1.TabIndex = 0;
        this.label1.Text = "System Rezerwacji Miejsc Kinowych";
        // 
        // lblPodtytul
        // 
        this.lblPodtytul.AutoSize = true;
        this.lblPodtytul.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblPodtytul.ForeColor = System.Drawing.Color.FromArgb(170, 175, 190);
        this.lblPodtytul.Location = new System.Drawing.Point(27, 45);
        this.lblPodtytul.Name = "lblPodtytul";
        this.lblPodtytul.Size = new System.Drawing.Size(332, 15);
        this.lblPodtytul.TabIndex = 1;
        this.lblPodtytul.Text = "Wybierz film, dogodny seans i zaznacz swoje miejsca na sali";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.label2.ForeColor = System.Drawing.Color.White;
        this.label2.Location = new System.Drawing.Point(25, 78);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(98, 17);
        this.label2.TabIndex = 2;
        this.label2.Text = "1. Wybierz film:";
        // 
        // listaFilmow
        // 
        this.listaFilmow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.listaFilmow.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        this.listaFilmow.FormattingEnabled = true;
        this.listaFilmow.Location = new System.Drawing.Point(25, 100);
        this.listaFilmow.Name = "listaFilmow";
        this.listaFilmow.Size = new System.Drawing.Size(235, 24);
        this.listaFilmow.TabIndex = 3;
        this.listaFilmow.SelectedIndexChanged += new System.EventHandler(this.listaFilmow_SelectedIndexChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.label3.ForeColor = System.Drawing.Color.White;
        this.label3.Location = new System.Drawing.Point(25, 135);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(107, 17);
        this.label3.TabIndex = 4;
        this.label3.Text = "2. Wybierz seans:";
        // 
        // listaSeansow
        // 
        this.listaSeansow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.listaSeansow.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        this.listaSeansow.FormattingEnabled = true;
        this.listaSeansow.Location = new System.Drawing.Point(25, 157);
        this.listaSeansow.Name = "listaSeansow";
        this.listaSeansow.Size = new System.Drawing.Size(235, 24);
        this.listaSeansow.TabIndex = 5;
        this.listaSeansow.SelectedIndexChanged += new System.EventHandler(this.listaSeansow_SelectedIndexChanged);
        // 
        // lblCzasTrwania
        // 
        this.lblCzasTrwania.AutoSize = true;
        this.lblCzasTrwania.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
        this.lblCzasTrwania.ForeColor = System.Drawing.Color.FromArgb(100, 181, 246);
        this.lblCzasTrwania.Location = new System.Drawing.Point(25, 190);
        this.lblCzasTrwania.Name = "lblCzasTrwania";
        this.lblCzasTrwania.Size = new System.Drawing.Size(89, 15);
        this.lblCzasTrwania.TabIndex = 6;
        this.lblCzasTrwania.Text = "Czas trwania: -";
        // 
        // txtOpis
        // 
        this.txtOpis.BackColor = System.Drawing.Color.FromArgb(42, 44, 57);
        this.txtOpis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.txtOpis.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        this.txtOpis.ForeColor = System.Drawing.Color.FromArgb(220, 220, 230);
        this.txtOpis.Location = new System.Drawing.Point(25, 212);
        this.txtOpis.Multiline = true;
        this.txtOpis.Name = "txtOpis";
        this.txtOpis.ReadOnly = true;
        this.txtOpis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtOpis.Size = new System.Drawing.Size(235, 75);
        this.txtOpis.TabIndex = 7;
        // 
        // pictureBox1
        // 
        this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(20, 21, 28);
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(25, 297);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(235, 225);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pictureBox1.TabIndex = 8;
        this.pictureBox1.TabStop = false;
        // 
        // lblNaglowekSali
        // 
        this.lblNaglowekSali.AutoSize = true;
        this.lblNaglowekSali.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblNaglowekSali.ForeColor = System.Drawing.Color.White;
        this.lblNaglowekSali.Location = new System.Drawing.Point(280, 78);
        this.lblNaglowekSali.Name = "lblNaglowekSali";
        this.lblNaglowekSali.Size = new System.Drawing.Size(183, 17);
        this.lblNaglowekSali.TabIndex = 9;
        this.lblNaglowekSali.Text = "3. Wybierz miejsca na sali:";
        // 
        // lblEkran
        // 
        this.lblEkran.BackColor = System.Drawing.Color.FromArgb(63, 66, 87);
        this.lblEkran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.lblEkran.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblEkran.ForeColor = System.Drawing.Color.White;
        this.lblEkran.Location = new System.Drawing.Point(280, 100);
        this.lblEkran.Name = "lblEkran";
        this.lblEkran.Size = new System.Drawing.Size(370, 26);
        this.lblEkran.TabIndex = 10;
        this.lblEkran.Text = "──────── EKRAN KINOWY ────────";
        this.lblEkran.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // panel1
        // 
        this.panel1.AutoScroll = true;
        this.panel1.BackColor = System.Drawing.Color.FromArgb(37, 40, 54);
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(280, 132);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(370, 260);
        this.panel1.TabIndex = 11;
        // 
        // panelLegenda
        // 
        this.panelLegenda.BackColor = System.Drawing.Color.FromArgb(42, 44, 57);
        this.panelLegenda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panelLegenda.Controls.Add(this.lblLegendaZajete);
        this.panelLegenda.Controls.Add(this.boxZajete);
        this.panelLegenda.Controls.Add(this.lblLegendaWybrane);
        this.panelLegenda.Controls.Add(this.boxWybrane);
        this.panelLegenda.Controls.Add(this.lblLegendaWolne);
        this.panelLegenda.Controls.Add(this.boxWolne);
        this.panelLegenda.Location = new System.Drawing.Point(280, 402);
        this.panelLegenda.Name = "panelLegenda";
        this.panelLegenda.Size = new System.Drawing.Size(370, 36);
        this.panelLegenda.TabIndex = 12;
        // 
        // lblLegendaZajete
        // 
        this.lblLegendaZajete.AutoSize = true;
        this.lblLegendaZajete.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblLegendaZajete.ForeColor = System.Drawing.Color.White;
        this.lblLegendaZajete.Location = new System.Drawing.Point(285, 9);
        this.lblLegendaZajete.Name = "lblLegendaZajete";
        this.lblLegendaZajete.Size = new System.Drawing.Size(39, 15);
        this.lblLegendaZajete.TabIndex = 5;
        this.lblLegendaZajete.Text = "Zajęte";
        // 
        // boxZajete
        // 
        this.boxZajete.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
        this.boxZajete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.boxZajete.Location = new System.Drawing.Point(263, 9);
        this.boxZajete.Name = "boxZajete";
        this.boxZajete.Size = new System.Drawing.Size(16, 16);
        this.boxZajete.TabIndex = 4;
        // 
        // lblLegendaWybrane
        // 
        this.lblLegendaWybrane.AutoSize = true;
        this.lblLegendaWybrane.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblLegendaWybrane.ForeColor = System.Drawing.Color.White;
        this.lblLegendaWybrane.Location = new System.Drawing.Point(155, 9);
        this.lblLegendaWybrane.Name = "lblLegendaWybrane";
        this.lblLegendaWybrane.Size = new System.Drawing.Size(53, 15);
        this.lblLegendaWybrane.TabIndex = 3;
        this.lblLegendaWybrane.Text = "Wybrane";
        // 
        // boxWybrane
        // 
        this.boxWybrane.BackColor = System.Drawing.Color.FromArgb(245, 158, 11);
        this.boxWybrane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.boxWybrane.Location = new System.Drawing.Point(133, 9);
        this.boxWybrane.Name = "boxWybrane";
        this.boxWybrane.Size = new System.Drawing.Size(16, 16);
        this.boxWybrane.TabIndex = 2;
        // 
        // lblLegendaWolne
        // 
        this.lblLegendaWolne.AutoSize = true;
        this.lblLegendaWolne.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblLegendaWolne.ForeColor = System.Drawing.Color.White;
        this.lblLegendaWolne.Location = new System.Drawing.Point(35, 9);
        this.lblLegendaWolne.Name = "lblLegendaWolne";
        this.lblLegendaWolne.Size = new System.Drawing.Size(41, 15);
        this.lblLegendaWolne.TabIndex = 1;
        this.lblLegendaWolne.Text = "Wolne";
        // 
        // boxWolne
        // 
        this.boxWolne.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
        this.boxWolne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.boxWolne.Location = new System.Drawing.Point(13, 9);
        this.boxWolne.Name = "boxWolne";
        this.boxWolne.Size = new System.Drawing.Size(16, 16);
        this.boxWolne.TabIndex = 0;
        // 
        // lblNaglowekKoszyka
        // 
        this.lblNaglowekKoszyka.AutoSize = true;
        this.lblNaglowekKoszyka.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblNaglowekKoszyka.ForeColor = System.Drawing.Color.White;
        this.lblNaglowekKoszyka.Location = new System.Drawing.Point(670, 78);
        this.lblNaglowekKoszyka.Name = "lblNaglowekKoszyka";
        this.lblNaglowekKoszyka.Size = new System.Drawing.Size(149, 17);
        this.lblNaglowekKoszyka.TabIndex = 13;
        this.lblNaglowekKoszyka.Text = "4. Twoje zamówienie:";
        // 
        // listBox1
        // 
        this.listBox1.BackColor = System.Drawing.Color.FromArgb(42, 44, 57);
        this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.listBox1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
        this.listBox1.ForeColor = System.Drawing.Color.White;
        this.listBox1.FormattingEnabled = true;
        this.listBox1.ItemHeight = 17;
        this.listBox1.Location = new System.Drawing.Point(670, 100);
        this.listBox1.Name = "listBox1";
        this.listBox1.Size = new System.Drawing.Size(225, 206);
        this.listBox1.TabIndex = 14;
        // 
        // lblPodsumowanie
        // 
        this.lblPodsumowanie.AutoSize = true;
        this.lblPodsumowanie.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        this.lblPodsumowanie.ForeColor = System.Drawing.Color.FromArgb(255, 213, 79);
        this.lblPodsumowanie.Location = new System.Drawing.Point(670, 316);
        this.lblPodsumowanie.Name = "lblPodsumowanie";
        this.lblPodsumowanie.Size = new System.Drawing.Size(144, 17);
        this.lblPodsumowanie.TabIndex = 15;
        this.lblPodsumowanie.Text = "Wybrano miejsc: 0";
        // 
        // btnUsunMiejsce
        // 
        this.btnUsunMiejsce.BackColor = System.Drawing.Color.FromArgb(70, 74, 96);
        this.btnUsunMiejsce.FlatAppearance.BorderSize = 0;
        this.btnUsunMiejsce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnUsunMiejsce.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        this.btnUsunMiejsce.ForeColor = System.Drawing.Color.White;
        this.btnUsunMiejsce.Location = new System.Drawing.Point(670, 345);
        this.btnUsunMiejsce.Name = "btnUsunMiejsce";
        this.btnUsunMiejsce.Size = new System.Drawing.Size(108, 30);
        this.btnUsunMiejsce.TabIndex = 16;
        this.btnUsunMiejsce.Text = "Usuń z listy";
        this.btnUsunMiejsce.UseVisualStyleBackColor = false;
        this.btnUsunMiejsce.Click += new System.EventHandler(this.btnUsunMiejsce_Click);
        // 
        // btnWyczyscKoszyk
        // 
        this.btnWyczyscKoszyk.BackColor = System.Drawing.Color.FromArgb(70, 74, 96);
        this.btnWyczyscKoszyk.FlatAppearance.BorderSize = 0;
        this.btnWyczyscKoszyk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnWyczyscKoszyk.Font = new System.Drawing.Font("Segoe UI", 8.5F);
        this.btnWyczyscKoszyk.ForeColor = System.Drawing.Color.White;
        this.btnWyczyscKoszyk.Location = new System.Drawing.Point(784, 345);
        this.btnWyczyscKoszyk.Name = "btnWyczyscKoszyk";
        this.btnWyczyscKoszyk.Size = new System.Drawing.Size(111, 30);
        this.btnWyczyscKoszyk.TabIndex = 17;
        this.btnWyczyscKoszyk.Text = "Wyczyść koszyk";
        this.btnWyczyscKoszyk.UseVisualStyleBackColor = false;
        this.btnWyczyscKoszyk.Click += new System.EventHandler(this.btnWyczyscKoszyk_Click);
        // 
        // btnZarezerwuj
        // 
        this.btnZarezerwuj.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
        this.btnZarezerwuj.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnZarezerwuj.FlatAppearance.BorderSize = 0;
        this.btnZarezerwuj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnZarezerwuj.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.btnZarezerwuj.ForeColor = System.Drawing.Color.White;
        this.btnZarezerwuj.Location = new System.Drawing.Point(670, 395);
        this.btnZarezerwuj.Name = "btnZarezerwuj";
        this.btnZarezerwuj.Size = new System.Drawing.Size(225, 60);
        this.btnZarezerwuj.TabIndex = 18;
        this.btnZarezerwuj.Text = "ZAREZERWUJ";
        this.btnZarezerwuj.UseVisualStyleBackColor = false;
        this.btnZarezerwuj.Click += new System.EventHandler(this.btnZarezerwuj_Click);
        // 
        // btnRaporty
        //
        this.btnRaporty.BackColor = System.Drawing.Color.FromArgb(70, 74, 96);
        this.btnRaporty.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnRaporty.FlatAppearance.BorderSize = 0;
        this.btnRaporty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnRaporty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.btnRaporty.ForeColor = System.Drawing.Color.White;
        this.btnRaporty.Location = new System.Drawing.Point(670, 462);
        this.btnRaporty.Name = "btnRaporty";
        this.btnRaporty.Size = new System.Drawing.Size(225, 35);
        this.btnRaporty.TabIndex = 19;
        this.btnRaporty.Text = "RAPORTY I STATYSTYKI";
        this.btnRaporty.UseVisualStyleBackColor = false;
        this.btnRaporty.Click += new System.EventHandler(this.btnRaporty_Click);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(30, 31, 41);
        this.ClientSize = new System.Drawing.Size(920, 545);
        this.Controls.Add(this.btnRaporty);
        this.Controls.Add(this.btnZarezerwuj);
        this.Controls.Add(this.btnWyczyscKoszyk);
        this.Controls.Add(this.btnUsunMiejsce);
        this.Controls.Add(this.lblPodsumowanie);
        this.Controls.Add(this.listBox1);
        this.Controls.Add(this.lblNaglowekKoszyka);
        this.Controls.Add(this.panelLegenda);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.lblEkran);
        this.Controls.Add(this.lblNaglowekSali);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.txtOpis);
        this.Controls.Add(this.lblCzasTrwania);
        this.Controls.Add(this.listaSeansow);
        this.Controls.Add(this.listaFilmow);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.lblPodtytul);
        this.Controls.Add(this.label1);
        this.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Kino - System Rezerwacji Miejsc (.NET 10)";
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.panelLegenda.ResumeLayout(false);
        this.panelLegenda.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblPodtytul;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox listaFilmow;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox listaSeansow;
    private System.Windows.Forms.Label lblCzasTrwania;
    private System.Windows.Forms.TextBox txtOpis;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lblNaglowekSali;
    private System.Windows.Forms.Label lblEkran;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panelLegenda;
    private System.Windows.Forms.Panel boxWolne;
    private System.Windows.Forms.Label lblLegendaWolne;
    private System.Windows.Forms.Panel boxWybrane;
    private System.Windows.Forms.Label lblLegendaWybrane;
    private System.Windows.Forms.Panel boxZajete;
    private System.Windows.Forms.Label lblLegendaZajete;
    private System.Windows.Forms.Label lblNaglowekKoszyka;
    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.Label lblPodsumowanie;
    private System.Windows.Forms.Button btnUsunMiejsce;
    private System.Windows.Forms.Button btnWyczyscKoszyk;
    private System.Windows.Forms.Button btnZarezerwuj;
    private System.Windows.Forms.Button btnRaporty;
}
