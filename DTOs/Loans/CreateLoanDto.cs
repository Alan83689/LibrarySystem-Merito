using System.ComponentModel.DataAnnotations;
namespace LibrarySystem.DTOs.Loans
{
    public class CreateLoanDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "BookId musi być większe od 0.")]
        public int BookId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "ReaderId musi być większe od 0.")]
        public int ReaderId { get; set; }
    }
}
