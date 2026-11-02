# Sprawozdanie - Tydzień 3

**Projekt:** System Rezerwacji Miejsc Kinowych
**Okres:** 2026-10-20 - 2026-10-26
**Repozytorium:** https://github.com/kacperbocian-boop/System_Rezerwacji

## Zakres prac wykonanych przez każdego członka zespołu

### Programista A (UI / Frontend)
- Modyfikacja `Form1.Designer.cs` - dodanie przycisku `btnRaporty` (225×35 px) w prawym dolnym rogu.
- Modyfikacja `Form1.cs` - obsługa zdarzenia `btnRaporty_Click` (otwarcie `FormRaporty`).
- Implementacja `FormRaporty.cs` (~80 linii):
  - wyświetlanie listy wszystkich rezerwacji,
  - wyświetlanie statystyk (liczba rezerwacji, filmów, seansów, najpopularniejszy film),
  - wyświetlanie obłożenia seansów (zajęte/wszystkie + procent).
- Implementacja `FormRaporty.Designer.cs` (~170 linii): pełen układ okna raportów.

### Programista B (Logika biznesowa)
- Implementacja `Logika/RezerwacjaService.cs` (~70 linii):
  - metoda `Zarezerwuj(Koszyk)` - zapis wszystkich pozycji z koszyka do bazy,
  - metoda `AnulujRezerwacje(reader, seansId, miejsceId)` - usuwa pojedynczą rezerwację,
  - metoda `AnulujWszystkieRezerwacje(reader, seansId)` - usuwa wszystkie rezerwacje na seans.
- Implementacja `Logika/RaportyService.cs` (~140 linii):
  - modele: `StatystykaFilmu`, `OblozenieSeansu`, `WpisRezerwacji`,
  - metody: `PobierzWszystkieRezerwacje`, `PoliczRezerwacje`, `PoliczFilmy`, `PoliczSeanse`, `NajpopularniejszyFilm`, `OblozenieSeanse`.
- Aktualizacja `Form1.cs` - integracja z `Koszyk`, `Walidacja`, `RezerwacjaService` (wymiana prostego `List<Miejsce>` na obiektową `Koszyk`).

### Programista C (Dane / Baza)
- Implementacja `db/EksportImport.cs` (~270 linii):
  - `EksportujFilmyDoCsv(string)` - eksport 4 kolumn (Id, Tytul, Opis, CzasTrwania, ImageURL) z prawidłowym escapowaniem cudzysłowów i przecinków.
  - `EksportujRezerwacjeDoCsv(string)` - eksport 6 kolumn (RezerwacjaId, FilmTytul, DataGodzina, Sala, Rzad, NumerMiejsca) z INNER JOIN do 3 tabel.
  - `EksportujWszystkoDoJson(string)` - pełny JSON (filmy, seanse, miejsca, rezerwacje) z datą eksportu i polskimi znakami (`UnsafeRelaxedJsonEscaping`).
  - `ImportujFilmyZCsv(string)` - import filmów, pomija duplikaty wg tytułu (case-insensitive).
  - Parser CSV obsługujący cudzysłowy i przecinki wewnątrz pól.

### Programista D (QA / Dokumentacja)
- Implementacja `Testy/RezerwacjaServiceTesty.cs` - 2 testy (null koszyk rzuca wyjątek, pusty koszyk zwraca 0).
- Końcowa wersja `Dokumentacja/DokumentacjaTechniczna.md` - pełny opis architektury 3-warstwowej, schemat bazy, API wszystkich warstw, tabele metod.
- Końcowa wersja `Dokumentacja/DokumentacjaUzytkownika.md` - instrukcja krok po kroku, screenshot, FAQ, rozwiązywanie problemów.
- Końcowa wersja `Dokumentacja/Naprawa_Issue_1.md` - szczegółowy opis naprawy problemu z `SQLitePCLRaw.batteries_v2.dll`.
- Implementacja diagramów UML w PlantUML:
  - `Dokumentacja/diagramy/DiagramPrzypadkowUzycia.puml` - 12 przypadków użycia, aktor Użytkownik.
  - `Dokumentacja/diagramy/DiagramKlas.puml` - wszystkie klasy z 3 warstw (UI / Logika / Dane), zależności, asocjacje.

## Postępy w realizacji poszczególnych funkcjonalności

| Funkcjonalność | Status |
|---|---|
| Repozytorium Git + README | DONE |
| Naprawa Issue #1 (SQLite) | DONE |
| Baza danych + dane startowe | DONE |
| Główny formularz (Form1) | DONE |
| Siatka miejsc (32 miejsca) | DONE |
| Koszyk rezerwacji (logika) | DONE |
| Walidacja danych | DONE |
| RezerwacjaService | DONE |
| Formularz raportów (FormRaporty) | DONE |
| Statystyki (RaportyService) | DONE |
| Eksport do CSV (filmy, rezerwacje) | DONE |
| Eksport do JSON (wszystkie dane) | DONE |
| Import z CSV (filmy) | DONE |
| Testy jednostkowe (30 testów) | DONE |
| Diagramy UML (przypadki użycia + klasy) | DONE |
| Dokumentacja techniczna | DONE |
| Dokumentacja użytkownika | DONE |

## Napotkane trudności i sposoby ich rozwiązania

1. **Brak UI do eksportu/importu CSV** - funkcje są w warstwie `EksportImport.cs` ale nie zostały podpięte do przycisków w UI. Decyzja zespołu: nie podpinamy w tym tygodniu, zostawiamy jako gotowe API dla przyszłych wersji. Testy jednostkowe weryfikują działanie.

2. **Polish znaki w JSON** - domyślnie `JsonSerializer` escape'uje znaki nie-ASCII (`\uXXXX`). Rozwiązano używając `JavaScriptEncoder.UnsafeRelaxedJsonEscaping` - teraz polskie znaki (`ł`, `ś`, `ń`) są zapisywane bezpośrednio.

3. **Wydajność `RaportyService.PobierzWszystkieRezerwacje`** - zliczanie rezerwacji wymaga przeiterowania wszystkich seansów. Dla większej ilości danych trzeba by napisać pojedynczy SQL z JOIN. W aktualnej wersji (3 rezerwacje, 8 seansów) wydajność jest wystarczająca.

4. **Diagram klas - konflikt stylu** - PlantUML domyślnie rysuje koło przy nazwie klasy. Wyłączono używając `hide circle` dla czystszej prezentacji.

## Plan na kolejny tydzień (Tydzień 4)

- **Programista D**: finalne testy - uruchomienie wszystkich 30 testów jednostkowych, weryfikacja wyników.
- **Wszyscy**: przegląd kodu - code review cross-zadaniowy (A sprawdza B, B sprawdza C, itd.).
- **Programista D**: złożenie 4 sprawozdań tygodniowych (T1-T4) w katalogu `Sprawozdania/`.
- **Wszyscy**: zamknięcie Issue #1 na GitHub z komentarzem o naprawie w `.csproj`.
- **Programista A**: test aplikacji end-to-end (uruchomienie, rezerwacja, anulowanie, raporty).
- **Programista D**: przygotowanie finalnej dokumentacji i README.
