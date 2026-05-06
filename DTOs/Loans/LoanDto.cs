namespace LibrarySystem.DTOs.Loans
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string TytulKsiazki { get; set; } = string.Empty;
        public int ReaderId { get; set; }
        public string Czytelnik { get; set; } = string.Empty;
        public DateTime DataWypozyczenia { get; set; }
        public bool CzyZwrocono { get; set; }
        public DateTime? DataZwrotu { get; set; }
    }
}
