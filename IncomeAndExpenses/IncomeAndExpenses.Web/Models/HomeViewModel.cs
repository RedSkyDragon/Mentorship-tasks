using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;

namespace IncomeAndExpenses.Web.Models
{
    public class HomeIndexViewModel
    {
        public User User { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Income> Incomes { get; set; }
    }
}