using System.ComponentModel.DataAnnotations;
namespace LibrarySystem.DTOs.Loans
{
    public class ReturnLoanDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id wypożyczenia musi być większe od 0.")]
        public int LoanId { get; set; }
    }
}
