# Dokumentacja techniczna - System Rezerwacji Miejsc Kinowych

## 1. Opis projektu

Aplikacja desktopowa w C# / WinForms do rezerwacji miejsc na seanse filmowe.
Zaprojektowana w architekturze trójwarstwowej:

```
+----------------+    +----------------+    +----------------+
|   WARSTWA UI    | -> |  WARSTWA LOGI  | -> |  WARSTWA DANY |
|   (Programista |    |   (Programista |    |  (Programista  |
|       A)       |    |       B)       |    |       C)       |
+----------------+    +----------------+    +----------------+
      Form1.cs            Koszyk.cs            db/Database.cs
      FormRaporty.cs      Walidacja.cs         db/DatabaseReader.cs
                          RezerwacjaService   db/DatabaseWriter.cs
                          RaportyService      db/EksportImport.cs
                          (Logika/)            (db/)
```

- **Warstwa UI (Programista A)** - klasy `Form1`, `FormRaporty`, pliki `.Designer.cs`. Odpowiada za wyświetlanie danych i obsługę kliknięć.
- **Warstwa logiki (Programista B)** - klasy `Koszyk`, `Walidacja`, `RezerwacjaService`, `RaportyService` w namespace `System_rezerwacji_miejsc.Logika`. Nie zależy od UI.
- **Warstwa danych (Programista C)** - klasy `Database`, `DatabaseReader`, `DatabaseWriter`, `EksportImport`, `Modele` w namespace `System_rezerwacji_miejsc.db`. Nie zależy od UI ani od logiki.

## 2. Architektura

### 2.1. Struktura katalogów

```
System rezerwacji miejsc/
├── Program.cs                         # Punkt wejścia
├── Form1.cs / .Designer.cs / .resx    # Główny formularz rezerwacji
├── FormRaporty.cs / .Designer.cs      # Formularz raportów
├── System rezerwacji miejsc.csproj    # Plik projektu
├── Properties/                        # Zasoby aplikacji
├── db/                                # Warstwa danych
│   ├── Modele.cs                      # Film, Seans, Miejsce
│   ├── Database.cs                    # Inicjalizacja bazy
│   ├── DatabaseReader.cs              # Zapytania SELECT
│   ├── DatabaseWriter.cs              # Zapytania INSERT/DELETE
│   └── EksportImport.cs               # CSV / JSON
├── Logika/                            # Warstwa logiki biznesowej
│   ├── Koszyk.cs                      # Logika koszyka
│   ├── Walidacja.cs                   # Walidacja danych
│   ├── RezerwacjaService.cs           # Koordynacja rezerwacji
│   └── RaportyService.cs              # Statystyki
├── Testy/                             # Testy jednostkowe xUnit
│   ├── KoszykTesty.cs
│   ├── WalidacjaTesty.cs
│   ├── RezerwacjaServiceTesty.cs
│   ├── DatabaseTesty.cs
│   └── SystemRezerwacjiTesty.csproj
└── Dokumentacja/                      # Ta dokumentacja
    ├── DokumentacjaTechniczna.md
    ├── DokumentacjaUzytkownika.md
    ├── Naprawa_Issue_1.md
    └── diagramy/
```

### 2.2. Schemat bazy danych (SQLite)

```sql
CREATE TABLE Filmy (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Tytul TEXT NOT NULL,
    Opis TEXT,
    CzasTrwania INTEGER NOT NULL,
    ImageURL TEXT
);

CREATE TABLE Seanse (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FilmId INTEGER NOT NULL,
    DataGodzina TEXT NOT NULL,
    Sala INTEGER NOT NULL
);

CREATE TABLE Miejsca (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Sala INTEGER NOT NULL,
    Rzad INTEGER NOT NULL,
    NumerMiejsca INTEGER NOT NULL
);

CREATE TABLE Rezerwacje (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SeansId INTEGER NOT NULL,
    MiejsceId INTEGER NOT NULL
);
```

Relacje:
- `Seanse.FilmId` -> `Filmy.Id` (1:N - jeden film, wiele seansów)
- `Miejsca.Sala` <-> `Seanse.Sala` (relacja logiczna przez numer sali)
- `Rezerwacje.SeansId` -> `Seanse.Id`, `Rezerwacje.MiejsceId` -> `Miejsca.Id` (M:N)

Dane startowe (automatycznie wstawiane przy pustej bazie):
- 4 filmy: Incepcja, Interstellar, Matrix, Mroczny Rycerz
- 8 seansów (po 2 na film) na salach 1 i 2
- 64 miejsca (2 sale × 32 foteli: 4 rzędy × 8 foteli)
- 3 przykładowe rezerwacje na seans 1 (miejsca 10, 11, 12)

## 3. Interfejs API (warstwa Logika)

### 3.1. Koszyk

| Metoda | Podpis | Opis |
|---|---|---|
| Dodaj | `void Dodaj(Miejsce, Seans)` | Dodaje miejsce do koszyka |
| Usun | `void Usun(Miejsce, Seans)` | Usuwa pojedynczą pozycję |
| Zawiera | `bool Zawiera(Miejsce, Seans)` | Czy pozycja jest już w koszyku |
| Wyczysc | `void Wyczysc()` | Czyści cały koszyk |
| Policz | `int Policz()` | Liczba pozycji |
| PobierzWszystkie | `IReadOnlyList<PozycjaKoszyka> PobierzWszystkie()` | Kopia listy pozycji |
| PobierzDlaSeansu | `IReadOnlyList<PozycjaKoszyka> PobierzDlaSeansu(int)` | Pozycje dla danego seansu |
| ZnajdzPoOpisie | `PozycjaKoszyka? ZnajdzPoOpisie(string)` | Szuka po opisie (z ListBox) |

### 3.2. Walidacja

| Metoda | Podpis | Opis |
|---|---|---|
| SprawdzKoszyk | `string SprawdzKoszyk(Koszyk, Seans)` | Walidacja koszyka przed rezerwacją |
| SprawdzFilm | `string SprawdzFilm(string, int)` | Walidacja danych filmu |
| SprawdzDateSeansu | `string SprawdzDateSeansu(string)` | Walidacja formatu daty |
| SprawdzSale | `string SprawdzSale(int)` | Walidacja numeru sali |

Zasada: pusty string = brak błędu, niepusty = komunikat błędu.

### 3.3. RezerwacjaService

| Metoda | Podpis | Opis |
|---|---|---|
| Zarezerwuj | `int Zarezerwuj(Koszyk)` | Zapisuje wszystkie pozycje z koszyka do bazy |
| AnulujRezerwacje | `bool AnulujRezerwacje(DatabaseReader, int, int)` | Usuwa pojedynczą rezerwację |
| AnulujWszystkieRezerwacje | `int AnulujWszystkieRezerwacje(DatabaseReader, int)` | Usuwa wszystkie rezerwacje na seans |

### 3.4. RaportyService

| Metoda | Podpis | Opis |
|---|---|---|
| PobierzWszystkieRezerwacje | `List<WpisRezerwacji> PobierzWszystkieRezerwacje()` | Lista rezerwacji z tytułem filmu |
| PoliczRezerwacje | `int PoliczRezerwacje()` | Liczba rezerwacji |
| PoliczFilmy | `int PoliczFilmy()` | Liczba filmów |
| PoliczSeanse | `int PoliczSeanse()` | Liczba seansów |
| NajpopularniejszyFilm | `StatystykaFilmu? NajpopularniejszyFilm()` | Film z największą liczbą rezerwacji |
| OblozenieSeanse | `List<OblozenieSeansu> OblozenieSeanse()` | Obłożenie wszystkich seansów |

## 4. Interfejs API (warstwa Danych)

### DatabaseReader

| Metoda | Podpis |
|---|---|
| PobierzWszystkieFilmy | `List<Film> PobierzWszystkieFilmy()` |
| PobierzFilmy | `List<string> PobierzFilmy()` |
| PobierzSeanseDlaFilmu | `List<Seans> PobierzSeanseDlaFilmu(int filmId)` |
| PobierzSeanse | `List<string> PobierzSeanse()` |
| PobierzObiektyMiejsc | `List<Miejsce> PobierzObiektyMiejsc(int sala)` |
| PobierzMiejsca | `List<int> PobierzMiejsca(int sala)` |
| PobierzZarezerwowaneMiejsca | `List<int> PobierzZarezerwowaneMiejsca(int seansId)` |
| PobierzObrazFilmu | `string PobierzObrazFilmu(int filmId)` |

### DatabaseWriter

| Metoda | Podpis |
|---|---|
| DodajFilm | `void DodajFilm(string, string, int, string)` |
| DodajSeans | `void DodajSeans(int, string, int)` |
| DodajMiejsce | `void DodajMiejsce(int, int, int)` |
| DodajRezerwacje | `void DodajRezerwacje(int seansId, int miejsceId)` |
| UsunRezerwacje | `void UsunRezerwacje(int seansId, int miejsceId)` |
| UsunWszystkieRezerwacje | `void UsunWszystkieRezerwacje(int seansId)` |

### EksportImport

| Metoda | Podpis |
|---|---|
| EksportujFilmyDoCsv | `void EksportujFilmyDoCsv(string sciezka)` |
| EksportujRezerwacjeDoCsv | `void EksportujRezerwacjeDoCsv(string sciezka)` |
| EksportujWszystkoDoJson | `void EksportujWszystkoDoJson(string sciezka)` |
| ImportujFilmyZCsv | `int ImportujFilmyZCsv(string sciezka)` |

## 5. Diagramy UML

Zob. `diagramy/DiagramPrzypadkowUzycia.puml` oraz `diagramy/DiagramKlas.puml`.
Diagramy w notacji PlantUML - można wyrenderować na https://planttext.com lub z pluginem w VS Code.

## 6. Testowanie

Projekt testowy `Testy/SystemRezerwacjiTesty.csproj` używa frameworka xUnit.
Uruchomienie testów z linii komend:

```bash
dotnet test Testy/SystemRezerwacjiTesty.csproj
```

Pokrycie testami:
- `KoszykTesty` - 9 testów (operacje koszyka)
- `WalidacjaTesty` - 13 testów (wszystkie metody walidacji)
- `DatabaseTesty` - 6 testów (inicjalizacja i odczyt)
- `RezerwacjaServiceTesty` - 2 testy (rezerwacje)

Łącznie: **30 testów jednostkowych**.

## 7. Konwencje kodu

- C# 13 / .NET 10, namespace w stylu `System_rezerwacji_miejsc.Logika`
- `Nullable` włączone, konsekwentne używanie `?` dla nullable
- `ImplicitUsings` włączone
- Nazwy metod po polsku (zgodnie z wymaganiami projektu)
- Nazwy klas po polsku (Koszyk, Walidacja, Database, DatabaseReader)
- Wszystkie metody w warstwie danych używają parametrów SQL (`@seansId`) zapobiegających SQL Injection

## 8. Znane problemy i rozwiązania

- **Issue #1**: `SQLitePCLRaw.batteries_v2.dll` nie kopiowany do folderu kompilacji. Rozwiązanie w `Naprawa_Issue_1.md`.
