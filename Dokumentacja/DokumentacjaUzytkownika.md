# Dokumentacja użytkownika - System Rezerwacji Miejsc Kinowych

## 1. Wprowadzenie

Aplikacja **System Rezerwacji Miejsc Kinowych** służy do rezerwowania miejsc na seanse filmowe w kinie. Użytkownik może:
- przeglądać listę dostępnych filmów wraz z opisami i plakatami,
- wybierać seans (dzień i godzina) dla danego filmu,
- zaznaczać wolne miejsca na sali kinowej,
- dodawać i usuwać miejsca w koszyku,
- ostatecznie zatwierdzać rezerwację,
- przeglądać raporty i statystyki.

## 2. Wymagania systemowe

- System operacyjny: Windows 10/11 (lub Linux/macOS z .NET 10 i Wine dla WinForms)
- .NET 10 Runtime (zgodny z projektem)
- Dostęp do internetu (opcjonalnie - do ładowania plakatów)
- ~50 MB wolnego miejsca na dysku

## 3. Instalacja

### 3.1. Z repozytorium Git

1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/kacperbocian-boop/System_Rezerwacji.git
   cd System_Rezerwacji
   ```

2. Zbuduj projekt:
   ```bash
   dotnet build
   ```

3. Uruchom aplikację:
   ```bash
   dotnet run --project "System rezerwacji miejsc.csproj"
   ```

Lub otwórz w Visual Studio 2022 i naciśnij F5.

### 3.2. Wersja prekompilowana

Pobierz paczkę ZIP, rozpakuj i uruchom `System rezerwacji miejsc.exe`.

## 4. Pierwsze uruchomienie

Przy pierwszym uruchomieniu aplikacja automatycznie:
1. Utworzy folder `data/` w katalogu programu.
2. Stworzy bazę SQLite `kino.db` z następującymi tabelami: `Filmy`, `Seanse`, `Miejsca`, `Rezerwacje`.
3. Wypełni bazę danymi startowymi:
   - 4 filmy (Incepcja, Interstellar, Matrix, Mroczny Rycerz),
   - 8 seansów na 2 salach (po 4 seanse na salę),
   - 64 miejsca (2 sale × 32 foteli),
   - 3 przykładowe rezerwacje.

## 5. Instrukcja obsługi

### 5.1. Główny ekran

Główne okno aplikacji podzielone jest na 3 sekcje:

1. **Lewy panel** - wybór filmu i seansu
2. **Środkowy panel** - siatka miejsc na sali kinowej
3. **Prawy panel** - koszyk rezerwacji

### 5.2. Krok 1 - Wybór filmu

1. W lewym górnym rogu znajdź rozwijaną listę **"1. Wybierz film:"**.
2. Kliknij listę i wybierz film (Incepcja / Interstellar / Matrix / Mroczny Rycerz).
3. Pod listą pojawi się:
   - opis filmu (w polu tekstowym),
   - czas trwania (np. "Czas trwania: 148 min"),
   - plakat filmu (ładowany z internetu).

### 5.3. Krok 2 - Wybór seansu

1. Poniżej pola wyboru filmu znajduje się lista **"2. Wybierz seans:"**.
2. Lista zawiera dostępne seanse dla wybranego filmu (data, godzina, sala).
3. Kliknij listę i wybierz dogodny seans.

### 5.4. Krok 3 - Wybór miejsc na sali

1. Po wybraniu seansu w środkowym panelu pojawi się siatka miejsc:
   - **ZIELONE** pola = wolne miejsca (można wybrać),
   - **CZERWONE** pola = zajęte (zablokowane, nie można kliknąć),
   - **ŻÓŁTE/POMARAŃCZOWE** = wybrane przez Ciebie, ale jeszcze nie zatwierdzone.
2. Poniżej siatki znajduje się **legenda** kolorów.
3. Kliknij zielone pole aby dodać miejsce do koszyka - zmieni kolor na żółty.
4. Kliknij żółte pole aby anulować wybór - wróci do zielonego.

### 5.5. Krok 4 - Koszyk rezerwacji

W prawym panelu:
- **"4. Twoje zamówienie:"** - lista wybranych miejsc (z numerem seansu i sali).
- **"Wybrano miejsc: N"** - aktualna liczba miejsc w koszyku.
- Przycisk **"Usuń z listy"** - usuwa wybrane miejsce z koszyka (zaznaczone na liście).
- Przycisk **"Wyczyść koszyk"** - usuwa WSZYSTKIE miejsca z koszyka jednocześnie.
- Przycisk **"ZAREZERWUJ"** (zielony, duży) - zatwierdza rezerwację i zapisuje do bazy.
- Przycisk **"RAPORTY I STATYSTYKI"** (na dole) - otwiera okno raportów.

### 5.6. Zatwierdzanie rezerwacji

1. Po wybraniu miejsc naciśnij **"ZAREZERWUJ"**.
2. Jeśli koszyk jest pusty, zobaczysz ostrzeżenie "Koszyk jest pusty. Zaznacz przynajmniej jedno wolne miejsce na sali."
3. Po pomyślnym zapisaniu pojawi się komunikat:
   ```
   Pomyślnie zarezerwowano N miejsc na seans:
   YYYY-MM-DD HH:MM (Sala X).
   ```
4. Siatka miejsc zostanie odświeżona - zarezerwowane miejsca zmienią kolor na czerwony.
5. Koszyk zostanie opróżniony.

### 5.7. Anulowanie rezerwacji

- **Pojedyncza pozycja w koszyku** (przed zatwierdzeniem): zaznacz na liście i kliknij "Usuń z listy".
- **Wszystkie pozycje w koszyku**: kliknij "Wyczyść koszyk".
- **Anulowanie zatwierdzonej rezerwacji**: w aktualnej wersji nie jest dostępne z poziomu UI (funkcja dostępna jest w warstwie logiki `RezerwacjaService.AnulujRezerwacje()` i może zostać dodana w przyszłych wersjach).

### 5.8. Raporty i statystyki

Naciśnij przycisk **"RAPORTY I STATYSTYKI"** w prawym dolnym rogu. Otworzy się osobne okno zawierające:

1. **Wszystkie rezerwacje** - lista wszystkich rezerwacji z bazy (id, film, data, sala, rząd, miejsce).
2. **Statystyki ogólne**:
   - liczba wszystkich rezerwacji,
   - liczba filmów w bazie,
   - liczba seansów,
   - najpopularniejszy film (z największą liczbą rezerwacji).
3. **Obłożenie seansów** - lista seansów z procentowym obłożeniem (np. "Incepcja | 2026-11-05 17:00 | Sala 1 | 5/32 (15.6%)").
4. Przycisk **"Odśwież"** - przeładowuje dane.
5. Przycisk **"Zamknij"** - zamyka okno raportów i wraca do ekranu rezerwacji.

## 6. Rozwiązywanie problemów

### Problem: Aplikacja nie startuje - błąd o brakującej bibliotece SQLite

**Przyczyna**: Znany problem (Issue #1) - biblioteka `SQLitePCLRaw.batteries_v2.dll` nie jest kopiowana do folderu kompilacji.

**Rozwiązanie**: Zob. `Dokumentacja/Naprawa_Issue_1.md`.

### Problem: Plakaty filmów nie ładują się

**Przyczyna**: Brak połączenia z internetem lub firewall blokuje połączenie do upload.wikimedia.org.

**Rozwiązanie**: Aplikacja automatycznie wyświetla szary plakat z napisem "FILM" i tytułem filmu zamiast obrazka. Działanie aplikacji nie jest naruszone.

### Problem: Baza danych została uszkodzona

**Rozwiązanie**: Zamknij aplikację. Usuń plik `data/kino.db` i `data/kino.db-journal`. Przy następnym uruchomieniu baza zostanie odtworzona z danymi startowymi.

### Problem: Wybrałem seans, ale nie ma miejsc na sali

**Przyczyna**: Sala dla tego seansu nie ma wpisanych miejsc w tabeli `Miejsca`.

**Rozwiązanie**: Dane startowe powinny utworzyć 32 miejsca na salę. Jeśli edytowałeś bazę ręcznie, dodaj rekordy do tabeli `Miejsca` dla odpowiedniej sali.

## 7. Skróty klawiszowe

| Skrót | Akcja |
|---|---|
| (brak - aplikacja obsługuje wszystko myszką) | - |

## 8. FAQ

**P: Czy mogę zarezerwować miejsca na kilku seansach jednocześnie?**
O: Nie. Koszyk resetuje się przy zmianie seansu. Zarezerwuj miejsca na jeden seans, a potem wybierz następny.

**P: Czy mogę edytować listę filmów?**
O: W aktualnej wersji nie. Funkcja jest dostępna w warstwie danych (`DatabaseWriter.DodajFilm`).

**P: Czy dane rezerwacji można eksportować?**
O: Tak - klasa `EksportImport` pozwala eksportować do CSV i JSON. Funkcja nie jest jeszcze podpięta pod przycisk w UI.

**P: Gdzie znajduje się plik bazy danych?**
O: W katalogu `<folder_aplikacji>/data/kino.db`.
