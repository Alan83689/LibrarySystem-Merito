namespace LibrarySystem.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LibraryCardNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public List<Loan> Loans { get; set; } = new();
    }
}
