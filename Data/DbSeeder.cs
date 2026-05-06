using LibrarySystem.Models;

namespace LibrarySystem.Data
{
    public static class DbSeeder
    {
        public static void Seed(LibraryDbContext db)
        {
            if (!db.Books.Any())
            {
                db.Books.AddRange(
                    new Book { Title = "Pan Tadeusz", Author = "Adam Mickiewicz", Isbn = "9788305134346", PublicationYear = 1834, AvailableCopies = 3 },
                    new Book { Title = "Lalka", Author = "Bolesław Prus", Isbn = "9788373271899", PublicationYear = 1890, AvailableCopies = 2 },
                    new Book { Title = "Quo Vadis", Author = "Henryk Sienkiewicz", Isbn = "9788306031187", PublicationYear = 1896, AvailableCopies = 1 }
                );
            }
            if (!db.Readers.Any())
            {
                db.Readers.AddRange(
                    new Reader { FirstName = "Jan", LastName = "Kowalski", LibraryCardNumber = "BK1001", Email = "jan.kowalski@example.com" },
                    new Reader { FirstName = "Anna", LastName = "Nowak", LibraryCardNumber = "BK1002", Email = "anna.nowak@example.com" }
                );
            }
            db.SaveChanges();
        }
    }
}
