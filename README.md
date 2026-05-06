# System biblioteczny – projekt zaliczeniowy .NET

## Autor
Alan Rogalski 83689

## Przedmiot
Programowanie .NET

## Grupa
ININ4(hybryda)_PR2.1

## Semestr
4

---

# Opis domeny

Projekt przedstawia system biblioteczny wykonany w technologii ASP.NET Core Web API.

Aplikacja umożliwia:
- zarządzanie książkami,
- zarządzanie czytelnikami,
- obsługę wypożyczeń.

System wykorzystuje architekturę warstwową:
- Controllers
- Services
- DTO
- Entity Framework Core
- SQLite

---

# Funkcjonalności

## ETAP I
- modele Book, Reader oraz Loan,
- DTO,
- endpointy GET i POST,
- relacje pomiędzy encjami.

## ETAP II
- Services,
- Dependency Injection,
- walidacja danych,
- endpointy PUT i DELETE,
- middleware obsługi błędów,
- reguły biznesowe.

## ETAP III
- EF Core,
- SQLite,
- paginacja,
- soft delete,
- Swagger XML,
- frontend HTML/CSS/JS,
- motyw WSB Merito.

---

# Reguły biznesowe

- maksymalnie 3 aktywne wypożyczenia,
- brak możliwości wypożyczenia niedostępnej książki,
- brak możliwości ponownego wypożyczenia tej samej książki.

---

# Konfiguracja bazy danych

Projekt wykorzystuje bazę SQLite.

Plik bazy:
```text
library.db
```

Connection string znajduje się w:
```text
appsettings.json
```

Przykład:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=library.db"
}
```

---

# Uruchomienie projektu

W terminalu:

```bash
dotnet restore
dotnet run
```

---

# Adresy

## Strona główna
```text
http://localhost:5091
```

## Swagger
```text
http://localhost:5091/swagger
```

---

# Repozytorium Git

https://github.com/Alan83689/LibrarySystem-Merito