# Sprawozdanie - Tydzień 2

**Projekt:** System Rezerwacji Miejsc Kinowych
**Okres:** 2026-10-13 - 2026-10-19
**Repozytorium:** https://github.com/kacperbocian-boop/System_Rezerwacji

## Zakres prac wykonanych przez każdego członka zespołu

### Programista A (UI / Frontend)
- Implementacja głównego formularza `Form1.cs` (~310 linii):
  - obsługa zdarzeń `listaFilmow_SelectedIndexChanged` i `listaSeansow_SelectedIndexChanged`,
  - dynamiczne tworzenie przycisków miejsc na sali (4 rzędy × 8 foteli = 32),
  - kolorowanie miejsc: zielone (wolne), czerwone (zajęte), żółte (wybrane),
  - ładowanie plakatów filmów z internetu (`PictureBox.LoadAsync`) z fallbackiem do rysowanego plakatu (`RysujZastepczyPlakat`).
- Implementacja `Form1.Designer.cs` (~410 linii): pełen układ paneli, legendy kolorów, przyciski.

### Programista B (Logika biznesowa)
- Implementacja `Logika/Koszyk.cs` (~90 linii):
  - klasa `PozycjaKoszyka` (Miejsce + Seans + Opis + Equals/GetHashCode),
  - metody: `Dodaj`, `Usun`, `Zawiera`, `Wyczysc`, `Policz`, `PobierzWszystkie`, `PobierzDlaSeansu`, `ZnajdzPoOpisie`.
- Implementacja `Logika/Walidacja.cs` (~90 linii):
  - metody: `SprawdzKoszyk`, `SprawdzFilm`, `SprawdzDateSeansu`, `SprawdzSale`,
  - konwencja: pusty string = OK, niepusty = komunikat błędu.

### Programista C (Dane / Baza)
- Implementacja `db/Modele.cs` - klasy: `Film`, `Seans`, `Miejsce`.
- Implementacja `db/Database.cs` - tworzenie bazy, tabel i 4 przykładowych filmów + 8 seansów + 64 miejsc + 3 rezerwacji.
- Implementacja `db/DatabaseReader.cs` - 8 metod odczytu z parametryzowanymi zapytaniami SQL.
- Implementacja `db/DatabaseWriter.cs` - 6 metod zapisu/usuwania z parametryzowanymi zapytaniami SQL.
- **Naprawa Issue #1**: dodanie `SQLitePCLRaw.bundle_e_sqlite3` v2.1.10 w `.csproj` + `IncludeAssets=all` dla `Microsoft.Data.Sqlite`. Wszystkie brakujące biblioteki SQLite zostały poprawnie kopiowane do `bin/Debug/net10.0-windows/`.

### Programista D (QA / Dokumentacja)
- Rozpoczęcie projektu testowego `Testy/SystemRezerwacjiTesty.csproj` (xUnit, .NET 10).
- Implementacja `Testy/DatabaseTesty.cs` - 6 testów (inicjalizacja bazy, odczyt filmów, seansów, miejsc).
- Implementacja `Testy/KoszykTesty.cs` - 9 testów (operacje koszyka, duplikaty, czyszczenie).
- Implementacja `Testy/WalidacjaTesty.cs` - 13 testów (walidacja koszyka, filmu, daty, sali).
- Rozpoczęcie dokumentacji technicznej (`DokumentacjaTechniczna.md`).

## Postępy w realizacji poszczególnych funkcjonalności

| Funkcjonalność | Status |
|---|---|
| Repozytorium Git + README | DONE |
| Plik `.csproj` .NET 10 WinForms | DONE |
| Naprawa Issue #1 (SQLite) | DONE |
| Schemat bazy SQLite | DONE |
| Modele (Film, Seans, Miejsce) | DONE |
| Operacje odczytu z bazy (DatabaseReader) | DONE |
| Operacje zapisu do bazy (DatabaseWriter) | DONE |
| Główny formularz (Form1) | DONE |
| Siatka miejsc (32 miejsca) | DONE |
| Koszyk - logika | DONE |
| Walidacja - logika | DONE |
| Eksport/import CSV / JSON | NIE ROZPOCZĘTY |
| Formularz raportów (FormRaporty) | NIE ROZPOCZĘTY |
| Statystyki (RaportyService) | NIE ROZPOCZĘTY |
| Testy jednostkowe | CZĘŚCIOWO (28 testów) |
| Diagramy UML | NIE ROZPOCZĘTE |
| Dokumentacja techniczna | CZĘŚCIOWO |

## Napotkane trudności i sposoby ich rozwiązania

1. **Issue #1 rozwiązany** - po dodaniu `SQLitePCLRaw.bundle_e_sqlite3` biblioteki SQLite poprawnie kopiowane są do `bin/Debug/net10.0-windows/`. Weryfikacja: aplikacja uruchamia się bez błędu o brakującej bibliotece. Issue #1 na GitHub zostanie zamknięte w tygodniu 4 po finalnej weryfikacji.

2. **Konflikt nullable w `Form1.cs`** - kilka razy `czytnik.PobierzZarezerwowaneMiejsca` zwracało `null` dla pustego seansu. Rozwiązano dodając wczesny return `if (listaSeansow.SelectedItem is not Seans wybranySeans) return;`.

3. **Brak plakatu gdy brak Internetu** - `PictureBox.LoadAsync` rzucał wyjątek. Rozwiązano podpięciem zdarzenia `LoadCompleted` z rysowaniem plakatu zastępczego (`RysujZastepczyPlakat`).

4. **Zawieszanie testów na CI** - niektóre testy korzystają z prawdziwej bazy i tworzą plik `kino.db`. Rozwiązano: każdy test woła `baza.UtworzBaze()` co jest idempotentne (`CREATE TABLE IF NOT EXISTS`).

## Plan na kolejny tydzień (Tydzień 3)

- **Programista A**: dodanie przycisku "RAPORTY I STATYSTYKI" w Form1, implementacja `FormRaporty.cs` i `FormRaporty.Designer.cs`.
- **Programista B**: implementacja `Logika/RezerwacjaService.cs` i `Logika/RaportyService.cs`.
- **Programista C**: implementacja `db/EksportImport.cs` (CSV + JSON).
- **Programista D**: implementacja `Testy/RezerwacjaServiceTesty.cs`, `Testy/EksportImportTesty.cs`, diagramy UML (`DiagramPrzypadkowUzycia.puml`, `DiagramKlas.puml`).
