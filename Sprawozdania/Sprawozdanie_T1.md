# Sprawozdanie - Tydzień 1

**Projekt:** System Rezerwacji Miejsc Kinowych
**Okres:** 2026-10-05 - 2026-10-12
**Repozytorium:** https://github.com/kacperbocian-boop/System_Rezerwacji

## Zakres prac wykonanych przez każdego członka zespołu

### Programista A (UI / Frontend)
- Stworzenie repozytorium na GitHub `kacperbocian-boop/System_Rezerwacji`.
- Inicjalizacja pliku README.md z tytułem projektu.
- Setup projektu .NET 10 WinForms w Visual Studio (`System rezerwacji miejsc.csproj`).
- Wstępna koncepcja układu głównego formularza (`Form1`) - 3 panele:
  - panel lewy: wybór filmu i seansu,
  - panel środkowy: siatka miejsc,
  - panel prawy: koszyk.

### Programista B (Logika biznesowa)
- Analiza wymagań funkcjonalnych projektu.
- Opracowanie planu klas logiki biznesowej: `Koszyk`, `Walidacja`, `RezerwacjaService`.
- Zdefiniowanie kontraktów metod (Podpis / Parametry / Zwracana wartość).
- Wybór wzorca architektonicznego: trójwarstwowy (UI / Logika / Dane).

### Programista C (Dane / Baza)
- Zapoznanie się z pakietem `Microsoft.Data.Sqlite` (commit `7cf8f6f`: "Zapoznanie sie z pakietem SQLite od Microsofta i opracowanie planu wykonania projektu").
- Projekt schematu bazy danych: tabele `Filmy`, `Seanse`, `Miejsca`, `Rezerwacje`.
- Wybór formatu pliku danych: SQLite (główny), CSV / JSON (export/import).

### Programista D (QA / Dokumentacja)
- Opracowanie dokumentacji wstępnej (ten plik + opisy zadań w README).
- Ustalenie harmonogramu 4-tygodniowego realizacji projektu.
- Konfiguracja GitHub Issues dla zgłaszania błędów.

## Postępy w realizacji poszczególnych funkcjonalności

| Funkcjonalność | Status |
|---|---|
| Repozytorium Git + README | DONE (100%) |
| Plik `.csproj` .NET 10 WinForms | DONE (100%) |
| Schemat bazy SQLite | DONE (na papierze) |
| Architektura (3 warstwy) | Zatwierdzona |
| Interfejs użytkownika (koncepcja) | Zaplanowany |
| Logika koszyka (koncepcja) | Zaplanowana |
| Testy jednostkowe | Nie rozpoczęte |

## Napotkane trudności i sposoby ich rozwiązania

1. **Problem z instalacją pakietu `Microsoft.Data.Sqlite`** - `SQLitePCLRaw.batteries_v2.dll` nie był kopiowany do folderu kompilacji. Zgłoszono **Issue #1** przez użytkownika PEB3332 (2026-10-12). Rozwiązanie zaplanowano na tydzień 2 - jawnie dodać pakiet `SQLitePCLRaw.bundle_e_sqlite3` i atrybut `IncludeAssets=all`.

2. **Wybór formatu danych** - zespół debatował między plikami CSV a bazą SQLite. Ostatecznie wybrano SQLite jako bazę główną + opcjonalny eksport do CSV / JSON.

3. **Brak dostępu do Internetu na niektórych maszynach zespołu** - utrudnia ładowanie plakatów filmów. Planowane rozwiązanie: fallback do rysowanego plakatu zastępczego w kodzie UI.

## Plan na kolejny tydzień (Tydzień 2)

- **Programista A**: implementacja `Form1.cs` i `Form1.Designer.cs` (wybór filmu, seansu, siatka 32 miejsc).
- **Programista B**: implementacja klas `Koszyk.cs` i `Walidacja.cs` w namespace `Logika`.
- **Programista C**: implementacja `Database.cs` (tworzenie tabel + dane startowe), `DatabaseReader.cs`, `DatabaseWriter.cs`, `Modele.cs`.
- **Programista D**: rozpoczęcie testów jednostkowych (xUnit), przygotowanie diagramów UML.
- **Naprawa Issue #1**: dodanie pakietu `SQLitePCLRaw.bundle_e_sqlite3` w `.csproj`.
