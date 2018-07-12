using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
