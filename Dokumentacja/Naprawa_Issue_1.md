# Naprawa Issue #1: .NET 4.8 / Microsoft SQLite Problem

## Problem (oryginalny opis)

> During installation of the Microsoft.SQLite package, there is a problem because the file
> **SQLitePCLRaw.batteries_v2.dll** is not being added to the project's compilation folder.

Źródło: https://github.com/kacperbocian-boop/System_Rezerwacji/issues/1
Zgłaszający: PEB3332, 12 października 2026

## Diagnoza

Pakiet `Microsoft.Data.Sqlite` ma zależność od `SQLitePCLRaw.bundle_*.bundle_*`. W niektórych konfiguracjach projektu (zwłaszcza gdy używamy SDK-style `.csproj` dla .NET 10) domyślny `PackageReference` nie kopiuje natywnych bibliotek (np. `e_sqlite3.dll`) i bibliotek mostkowych (`SQLitePCLRaw.batteries_v2.dll`) do folderu `bin/Debug/net10.0-windows/`.

## Rozwiązanie

### Krok 1: Modyfikacja pliku `.csproj`

W pliku `System rezerwacji miejsc.csproj` dodaj jawny pakiet `SQLitePCLRaw.bundle_e_sqlite3` oraz wymuś `IncludeAssets=all` dla `Microsoft.Data.Sqlite`:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Data.Sqlite" Version="10.0.14">
    <IncludeAssets>all</IncludeAssets>
  </PackageReference>
  <PackageReference Include="SQLitePCLRaw.bundle_e_sqlite3" Version="2.1.10" />
</ItemGroup>
```

`IncludeAssets=all` wymusza skopiowanie WSZYSTKICH zasobów pakietu (w tym `lib/` i `build/`) do folderu kompilacji.

`SQLitePCLRaw.bundle_e_sqlite3` dostarcza:
- `SQLitePCLRaw.batteries_v2.dll` (brakujący plik z Issue #1),
- `SQLitePCLRaw.core.dll`,
- `SQLitePCLRaw.provider.e_sqlite3.dll`,
- natywną bibliotekę `e_sqlite3.dll` (odpowiednią dla platformy).

### Krok 2: Czyszczenie i odbudowa

Po modyfikacji `.csproj` wykonaj:

```bash
dotnet clean
dotnet restore
dotnet build
```

W Visual Studio: Build -> Clean Solution, a następnie Build -> Rebuild Solution.

### Krok 3: Weryfikacja

Po odbudowie w folderze `bin/Debug/net10.0-windows/` powinny znaleźć się następujące pliki:

```
SQLitePCLRaw.batteries_v2.dll       <-- ten plik brakował
SQLitePCLRaw.core.dll
SQLitePCLRaw.provider.e_sqlite3.dll
e_sqlite3.dll                       <-- natywna biblioteka SQLite
Microsoft.Data.Sqlite.dll
Microsoft.Data.Sqlite.xml
runtimes/win-x64/native/e_sqlite3.dll   <-- ...oraz odpowiedniki dla każdej platformy
```

### Krok 4: Test działania

Uruchom aplikację. Powinna się otworzyć bez błędu o brakującej bibliotece. Baza SQLite tworzy się automatycznie w `data/kino.db`.

## Weryfikacja rozwiązania

Po wykonaniu powyższych kroków problem z Issue #1 jest rozwiązany.
Należy zamknąć issue komentarzem:

```
Zamknięte w commicie <SHA-commit>. Rozwiązanie: dodanie jawnego pakietu
SQLitePCLRaw.bundle_e_sqlite3 oraz atrybutu IncludeAssets=all do Microsoft.Data.Sqlite.
Szczegóły w Dokumentacja/Naprawa_Issue_1.md.
```

Komenda git do zamknięcia issue z poziomu CLI:

```bash
gh issue close 1 --repo kacperbocian-boop/System_Rezerwacji \
  --comment "Zamknięte - naprawiono w .csproj dodając SQLitePCLRaw.bundle_e_sqlite3 i IncludeAssets=all. Patrz Dokumentacja/Naprawa_Issue_1.md."
```

(Wymaga zainstalowanego GitHub CLI `gh`).
