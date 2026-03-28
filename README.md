# Wprawki z Technologii .NET - System Zarządzania Energią (VoltShare)

Repozytorium zawiera realizację zadań laboratoryjnych (Wprawek) w technologii ASP.NET Core MVC. Projekt symuluje system zarządzania klastrami energii oraz urządzeniami prosumentów.

---

## Technologie i wymagania
- **Framework:** .NET 8.0 / 9.0 (ASP.NET Core MVC)
- **ORM:** Entity Framework Core
- **Baza danych:** SQL Server (LocalDB)
- **Narzędzia:** Visual Studio 2022, SQL Server Object Explorer

---

## Struktura Projektu
- `/Wprawka_1` - Podstawowe modelowanie bazy danych, relacje i migracje.
- `/Wprawka_2` - Logika biznesowa, pełny CRUD, obsługa sesji i filtrowanie danych.

---

## Wprawka 1: Modelowanie i Relacje
**Cel:** Zaprojektowanie relacyjnej bazy danych przy użyciu podejścia *Code-First*.

### Kluczowe elementy:
1. **Modelowanie encji:** Zaimplementowano klasy `User`, `Home` (Nieruchomość) oraz `Cluster` (Klaster Energii).
2. **Relacja One-to-Many (1..N):** Jeden użytkownik może posiadać wiele nieruchomości (`User` -> `Homes`).
3. **Relacja Many-to-Many (N..M):** Użytkownik może należeć do wielu klastrów, a klaster zrzesza wielu użytkowników. Zrealizowane za pomocą **Fluent API** w klasie `DbContext`.
4. **Ograniczenia:** Użyto `DataAnnotations` (`[Required]`, `[MaxLength]`, `[EmailAddress]`) do walidacji schematu bazy danych.

> **Jak powstało:** Najpierw zdefiniowałem klasy modeli w C#. Następnie za pomocą `Add-Migration` wygenerowałem skrypty SQL, a `Update-Database` utworzyło fizyczną bazę danych z kluczami obcymi i tabelą pośrednią `UserClusterMemberships`.

![Screenshot Wprawka 1 - Diagram Bazy](/images/wprawka1.png)
*(Opis: Widok tabel w SQL Server Object Explorer)*

---

## Wprawka_2: CRUD, Filtrowanie i Sesja
**Cel:** Implementacja interfejsu użytkownika do zarządzania urządzeniami oraz zaawansowana logika filtrowania.

### Kluczowe funkcjonalności:
1. **Pełny CRUD:** Operacje tworzenia, odczytu, edycji i usuwania urządzeń (`Devices`).
2. **Relacje w formularzach:** Dynamiczny dropdown w formularzu tworzenia urządzenia, pobierający listę dostępnych klastrów z bazy danych.
3. **Filtrowanie danych:** Możliwość przeglądania urządzeń przypisanych do konkretnego klastra.
4. **Zarządzanie stanem (Sesja):** Zastosowanie `HttpContext.Session`. System zapamiętuje ostatnio wybrany filtr klastra, nawet po przejściu na inną podstronę.
5. **Walidacja wejściowa:** Po stronie serwera i klienta (np. sprawdzanie zakresu mocy urządzenia `[Range(1, 10000)]`).

> **Jak powstało:** Użyłem mechanizmu **Scaffoldingu**, aby wygenerować bazowe widoki. Następnie ręcznie zmodyfikowałem metodę `Index` w `DevicesController`, dodając logikę `HttpContext.Session.SetInt32`, która przechowuje ID wybranego klastra.

![Screenshot Wprawka 2 - Tabela z filtrem](/images/wprawka2.png)
*(Opis: Strona główna z panelami filtrowania i tabelą urządzeń)*

---

## Instrukcja Uruchomienia
1. Sklonuj repozytorium.
2. Otwórz wybrany projekt (`.sln`) w Visual Studio.
3. W **Konsoli Menedżera Pakietów** uruchom komendę: `Update-Database`, aby utworzyć lokalną bazę danych.
4. Uruchom projekt (F5). Baza zostanie automatycznie zasilona danymi testowymi dzięki metodzie `HasData` w `AppDbContext`.

---

## Ściąga do prezentacji (Ewentualne pytania)

| O co może zapytać prowadząca? | Twoja odpowiedź |
| :--- | :--- |
| **Gdzie jest Many-to-Many?** | W `AppDbContext.cs` w metodzie `OnModelCreating` zdefiniowałem relację `User` <-> `Cluster` za pomocą Fluent API. |
| **Jak działa zapamiętywanie filtra?** | Użyłem mechanizmu Sesji. W kontrolerze sprawdzam `HttpContext.Session.GetInt32("SavedFilter")`. Jeśli tam jest wartość, aplikacja filtruje listę automatycznie. |
| **Jak zarejestrowałeś sesję?** | W pliku `Program.cs` dodałem `builder.Services.AddSession()` oraz wywołałem middleware `app.UseSession()` przed autoryzacją. |
| **Jak wymusiłeś walidację?** | W modelu `Device` dodałem atrybuty `[Required]` i `[Range]`. W kontrolerze sprawdzam `if (ModelState.IsValid)` przed zapisem do bazy. |