# System Rezerwacji Miejsc Kinowych

Aplikacja desktopowa (C# / WinForms / .NET 10) do rezerwacji miejsc na seanse filmowe.

## Zespół

| Osoba            | Rola              | Zakres obowiązków                                                          |
|------------------|-------------------|----------------------------------------------------------------------------|
| Programista A    | UI / Frontend     | Tworzenie interfejsu użytkownika, obsługa formularzy, interakcja z aplikacją |
| Programista B    | Logika biznesowa  | Obsługa koszyka, rezerwacje, anulowanie, walidacja danych                    |
| Programista C    | Dane / Baza       | Operacje na bazie SQLite, import/eksport CSV / JSON                         |
| Programista D    | QA / Dokumentacja | Testy jednostkowe, dokumentacja techniczna i użytkownika, raporty            |

## Technologie

- **Język:** C# 13 / .NET 10
- **UI:** Windows Forms (WinForms)
- **Baza danych:** SQLite (przez `Microsoft.Data.Sqlite`)
- **Formaty danych:** SQLite (główny), CSV, JSON (eksport/import)
- **IDE:** Visual Studio 2022 / Rider
- **Kontrola wersji:** Git + GitHub

## Struktura projektu

```
System rezerwacji miejsc/
├── Program.cs                  # Punkt wejścia aplikacji
├── Form1.cs / .Designer.cs / .resx   # Główny formularz rezerwacji
├── FormRaporty.cs / .Designer.cs     # Formularz raportów i statystyk
├── System rezerwacji miejsc.csproj  # Plik projektu
├── Properties/
├── db/
│   ├── Modele.cs                # Klasy: Film, Seans, Miejsce
│   ├── Database.cs              # Inicjalizacja bazy
│   ├── DatabaseReader.cs        # Odczyt danych
│   ├── DatabaseWriter.cs        # Zapis danych
│   └── EksportImport.cs         # Eksport/import CSV / JSON
├── Logika/
│   ├── Koszyk.cs                # Logika koszyka rezerwacji
│   ├── RezerwacjaService.cs     # Koordynacja rezerwacji
│   ├── Walidacja.cs             # Walidacja danych wejściowych
│   └── RaportyService.cs        # Statystyki i raporty
├── Testy/                       # Testy jednostkowe xUnit
└── Dokumentacja/                # Dokumentacja techniczna, użytkownika, UML
    └── Sprawozdania/            # Cotygodniowe sprawozdania
```

## Funkcjonalności

1. Wczytywanie danych o filmach, seansach i miejscach z bazy SQLite.
2. Wyświetlanie dostępnych miejsc na dany seans (sala 4 rzędy × 8 foteli = 32 miejsca).
3. Koszyk rezerwacji: dodawanie, usuwanie pojedynczych miejsc, czyszczenie całego koszyka.
4. Zatwierdzanie rezerwacji – zapis do bazy, miejsca stają się zajęte.
5. Anulowanie rezerwacji (pojedynczej lub wszystkich dla seansu).
6. Import / eksport danych do CSV i JSON.
7. Generowanie raportów: lista wszystkich rezerwacji, statystyki obłożenia sali, frekwencja filmów.

## Uruchomienie

```bash
git clone https://github.com/kacperbocian-boop/System_Rezerwacji.git
cd System_Rezerwacji
dotnet build
dotnet run --project "System rezerwacji miejsc.csproj"
```

Baza SQLite (`kino.db`) tworzona jest automatycznie przy pierwszym uruchomieniu w podkatalogu `data/`, razem z przykładowymi danymi (4 filmy, 8 seansów, 64 miejsca w 2 salach, 3 przykładowe rezerwacje).

## Naprawa znanego problemu (Issue #1)

`SQLitePCLRaw.batteries_v2.dll` nie jest kopiowany do folderu kompilacji po dodaniu `Microsoft.Data.Sqlite`. Rozwiązanie – w pliku `.csproj` dodać:

```xml
<PackageReference Include="Microsoft.Data.Sqlite" Version="10.0.14">
  <IncludeAssets>all</IncludeReference>
</PackageReference>
<PackageReference Include="SQLitePCLRaw.bundle_e_sqlite3" Version="2.1.10" />
```

Szczegóły w `Dokumentacja/Naprawa_Issue_1.md`.

## Licencja

Projekt edukacyjny – wszystkie prawa zastrzeżone dla zespołu projektowego.
