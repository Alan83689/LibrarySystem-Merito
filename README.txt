SYSTEM BIBLIOTECZNY - PROJEKT FINALNY (ETAPY 1, 2, 3)

1. WYMAGANIA
- .NET 8 SDK x64
- Visual Studio 2022 lub VS Code z C# Dev Kit

2. URUCHOMIENIE
- Otwórz folder LibrarySystem
- W terminalu wpisz:
  dotnet restore
  dotnet run

3. ADRESY
- Strona główna:
  http://localhost:5091
- Swagger:
  http://localhost:5091/swagger

4. ZAWARTOŚĆ ETAPÓW
ETAP 1
- modele: Book, Reader, Loan
- DTO
- endpointy GET, GET po id, POST
- relacja logiczna Book -> Loan <- Reader

ETAP 2
- serwisy i interfejsy
- dependency injection
- walidacja Data Annotations
- PUT, DELETE
- global error handling middleware
- reguły biznesowe

ETAP 3
- EF Core + SQLite
- paginacja
- soft delete dla Book i Reader
- pełny Swagger z XML comments
- README

5. REGUŁY BIZNESOWE
- nie można wypożyczyć książki, jeśli brak egzemplarzy
- czytelnik może mieć maksymalnie 3 aktywne wypożyczenia
- czytelnik nie może wypożyczyć tej samej książki drugi raz, jeśli jej nie zwrócił
- nie można usunąć książki lub czytelnika z aktywnymi wypożyczeniami
- nie można usunąć aktywnego wypożyczenia bez wcześniejszego zwrotu

6. DODATKI Z ETAPU 3
- paginacja
- soft delete

7. WYGLĄD STRONY
- motyw biało-granatowy
- dodane logo Uniwersytety WSB Merito na stronie głównej
