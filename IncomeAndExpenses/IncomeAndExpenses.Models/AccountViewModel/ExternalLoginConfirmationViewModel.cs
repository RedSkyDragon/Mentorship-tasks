using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
