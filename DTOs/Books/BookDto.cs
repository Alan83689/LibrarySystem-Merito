namespace LibrarySystem.DTOs.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Tytul { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int RokWydania { get; set; }
        public int DostepneEgzemplarze { get; set; }
    }
}
