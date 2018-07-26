using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for Expense Type
    /// </summary>
    public class ExpenseTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer than 30 characters")]
        [MinLength(1, ErrorMessage = "Name should be longer than 1 character")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}