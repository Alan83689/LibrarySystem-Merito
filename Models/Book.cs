namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
        public bool IsDeleted { get; set; }
        public List<Loan> Loans { get; set; } = new();
    }
}
