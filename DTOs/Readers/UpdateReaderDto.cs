using System.ComponentModel.DataAnnotations;
namespace LibrarySystem.DTOs.Readers
{
    public class UpdateReaderDto
    {
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer karty jest wymagany.")]
        [StringLength(30, ErrorMessage = "Numer karty może mieć maksymalnie 30 znaków.")]
        public string LibraryCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        public string Email { get; set; } = string.Empty;
    }
}
