namespace LibrarySystem.DTOs.Readers
{
    public class ReaderDto
    {
        public int Id { get; set; }
        public string ImieNazwisko { get; set; } = string.Empty;
        public string NumerKarty { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
