# Sprawozdanie - Tydzień 4 (finalne)

**Projekt:** System Rezerwacji Miejsc Kinowych
**Okres:** 2026-10-27 - 2026-11-02
**Repozytorium:** https://github.com/kacperbocian-boop/System_Rezerwacji

## Zakres prac wykonanych przez każdego członka zespołu

### Programista A (UI / Frontend)
- Finalny test głównego formularza - uruchomienie aplikacji, weryfikacja wszystkich interakcji.
- Sprawdzenie działania przycisku "RAPORTY I STATYSTYKI" - poprawnie otwiera `FormRaporty`.
- Weryfikacja ładowania plakatów (zarówno z Internetu, jak i fallback do plakatu zastępczego przy braku sieci).
- Code review plików `Form1.cs` i `Form1.Designer.cs`.
- Aktualizacja `README.md` - pełny opis projektu, struktury katalogów, uruchomienia, naprawy Issue #1.

### Programista B (Logika biznesowa)
- Finalny test klas logiki - weryfikacja `Koszyk`, `Walidacja`, `RezerwacjaService`, `RaportyService`.
- Weryfikacja działania walidacji koszyka w scenariuszach:
  - pusty koszyk + brak seansu -> komunikat błędu,
  - pusty koszyk + wybrany seans -> komunikat "Koszyk jest pusty",
  - poprawny koszyk + wybrany seans -> rezerwacja zapisana do bazy.
- Code review warstwy logiki.
- Weryfikacja, że `Koszyk` jest idempotentny (dodanie tej samej pozycji dwa razy = 1 pozycja).

### Programista C (Dane / Baza)
- Finalny test bazy danych:
  - uruchomienie aplikacji na czystym środowisku - baza tworzy się automatycznie,
  - weryfikacja 4 tabel, 4 filmów, 8 seansów, 64 miejsc, 3 rezerwacji startowych,
  - test operacji zapisu/usuwania rezerwacji,
  - test eksportu do CSV (3 kolumny nagłówkowe + rekordy),
  - test eksportu do JSON (poprawny JSON z polskimi znakami),
  - test importu CSV (pomija duplikaty wg tytułu).
- **Zamknięcie Issue #1**: dodano komentarz do issue #1 na GitHub z odniesieniem do `Dokumentacja/Naprawa_Issue_1.md`.

### Programista D (QA / Dokumentacja)
- Uruchomienie wszystkich 30 testów jednostkowych - wynik: **30/30 PASSED**:
  - `KoszykTesty` - 9 testów PASSED,
  - `WalidacjaTesty` - 13 testów PASSED,
  - `DatabaseTesty` - 6 testów PASSED,
  - `RezerwacjaServiceTesty` - 2 testy PASSED.
- Weryfikacja diagramów UML - oba diagramy poprawnie renderują się na https://planttext.com.
- Skompletowanie i plikowanie 4 sprawozdań tygodniowych (`Sprawozdanie_T1.md` .. `Sprawozdanie_T4.md`).
- Weryfikacja dokumentacji - 3 pliki `.md` w `Dokumentacja/` + 2 diagramy `.puml` w `Dokumentacja/diagramy/`.
- Finalna kompilacja projektu bez ostrzeżeń (`dotnet build` -> 0 errors, 0 warnings).

## Postępy w realizacji poszczególnych funkcjonalności (finalna tabela)

| Funkcjonalność wg specyfikacji | Status |
|---|---|
| 1. Wczytywanie danych z pliku/CSV/bazy | DONE (SQLite) |
| 2. Wyświetlanie dostępnych miejsc na seans | DONE |
| 3. Koszyk - dodawanie miejsc | DONE |
| 3. Koszyk - usuwanie pojedynczych miejsc | DONE |
| 3. Koszyk - czyszczenie całego koszyka | DONE |
| 4. Zatwierdzenie rezerwacji -> zapis w bazie | DONE |
| 5. Usuwanie miejsc z koszyka przed zatwierdzeniem | DONE |
| 6. Zapisywanie i odczytywanie rezerwacji | DONE |
| 7. Generowanie raportów - przegląd rezerwacji | DONE |
| 7. Generowanie raportów - statystyki | DONE |
| **Bonus**: Eksport do CSV | DONE |
| **Bonus**: Eksport do JSON | DONE |
| **Bonus**: Import z CSV | DONE |
| **Bonus**: 30 testów jednostkowych | DONE |
| **Bonus**: Diagramy UML | DONE |
| **Bonus**: Naprawa Issue #1 | DONE |

**Wszystkie funkcjonalności z wymaganej specyfikacji zostały zrealizowane w 100%.**

## Napotkane trudności i sposoby ich rozwiązania

1. **Code review cross-zadaniowy ujawnił drobne uchybienia**:
   - W `Form1.cs` została nieużywana zmienna `wybranySeans` w jednej z metod - usunięto.
   - W `Walidacja.cs` brakowało sprawdzenia `null` w `SprawdzKoszyk` dla `koszyk` - dodano.
   - W `EksportImport.cs` kodowanie pliku domyślnie było ANSI - zmieniono na UTF-8.

2. **Testy jednostkowe zależne od bazy** - niektóre testy w `DatabaseTesty` tworzyły bazę w katalogu aplikacji, co utrudniało równoległe uruchomienie. Rozwiązano dodając `baza.UtworzBaze()` na początku każdego testu (operacja idempotentna).

3. **Issue #1 oficjalnie zamknięty** - komentarz wstawiony na GitHub. Commit zawiera odniesienie do `Dokumentacja/Naprawa_Issue_1.md`.

4. **Brak dostępu do GitHub z niektórych maszyn** - komendy git dla wszystkich członków zespołu przygotowano w plikach `PROGRAMISTA_*/komendy.txt`, co pozwala jednolite wklejanie do terminala.

## Ocena kryteriów projektowych (samoocena zespołu)

| Kryterium | Samoocena |
|---|---|
| Kompletność funkcjonalności | 100% - wszystkie wymagania specyfikacji zrealizowane |
| Jakość kodu (czytelność, modularność) | 95% - 3-warstwowa architektura, nazewnictwo polskie, parametryzowane SQL |
| Dokumentacja | 100% - techniczna, użytkownika, UML, naprawa issue, sprawozdania |
| Regularność sprawozdań | 100% - 4 sprawozdania tygodniowe (T1-T4) |
| Współpraca zespołowa | 95% - jasny podział ról, code review cross-zadaniowy |

## Plan na przyszłość (poza zakresem projektu)

Funkcje zaimplementowane w kodzie ale niepodpięte do UI:
- Eksport/import CSV / JSON (dostępne przez `EksportImport.cs`, do podpięcia pod przyciski w przyszłych wersjach).
- Anulowanie zatwierdzonych rezerwacji (dostępne przez `RezerwacjaService.AnulujRezerwacje`, do dodania w UI).
- Dodawanie nowych filmów (dostępne przez `DatabaseWriter.DodajFilm`, do dodania w UI).

## Podsumowanie

Projekt zakończony pomyślnie. Wszystkie funkcjonalności z wymaganej specyfikacji zostały zrealizowane, dodatkowo zaimplementowano 4 funkcje bonusowe (eksport/import, testy, UML, naprawa issue). Aplikacja uruchamia się na .NET 10 WinForms, baza SQLite tworzy się automatycznie z danymi startowymi. Wszystkie 30 testów jednostkowych przechodzi. Dokumentacja techniczna i użytkownika kompletna.
