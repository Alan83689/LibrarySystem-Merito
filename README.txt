SYSTEM BIBLIOTECZNY – PROJEKT ZALICZENIOWY .NET

Autor:
Alan Rogalski 83689

Przedmiot:
Programowanie .NET

Grupa:
ININ4(hybryda)_PR2.1

Semestr:
4

Opis:
Projekt przedstawia system biblioteczny wykonany w technologii ASP.NET Core Web API.
Aplikacja umożliwia zarządzanie książkami, czytelnikami oraz wypożyczeniami.

Technologie:
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger
- HTML, CSS, JavaScript

Zakres projektu:
Etap I:
- modele Book, Reader, Loan
- DTO
- endpointy GET i POST
- relacja między książką, czytelnikiem i wypożyczeniem

Etap II:
- warstwa Services
- Dependency Injection
- walidacja danych
- endpointy PUT i DELETE
- middleware obsługi błędów
- reguły biznesowe

Etap III:
- EF Core + SQLite
- paginacja
- soft delete
- Swagger z dokumentacją XML
- frontend z motywem WSB Merito

Uruchomienie:
1. Otwórz folder projektu.
2. W terminalu wpisz:
   dotnet restore
   dotnet run

Adresy:
Strona:
http://localhost:5091

Swagger:
http://localhost:5091/swagger

Repozytorium:
https://github.com/Alan83689/LibrarySystem-Merito