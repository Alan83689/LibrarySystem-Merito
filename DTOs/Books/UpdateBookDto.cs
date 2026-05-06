using System.ComponentModel.DataAnnotations;
namespace LibrarySystem.DTOs.Books
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [StringLength(120, ErrorMessage = "Tytuł może mieć maksymalnie 120 znaków.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Autor jest wymagany.")]
        [StringLength(100, ErrorMessage = "Autor może mieć maksymalnie 100 znaków.")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN jest wymagany.")]
        [StringLength(20, ErrorMessage = "ISBN może mieć maksymalnie 20 znaków.")]
        public string Isbn { get; set; } = string.Empty;

        [Range(1450, 2100, ErrorMessage = "Rok wydania jest niepoprawny.")]
        public int PublicationYear { get; set; }

        [Range(0, 100, ErrorMessage = "Liczba egzemplarzy musi być z zakresu 0-100.")]
        public int AvailableCopies { get; set; }
    }
}
