using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;

namespace IncomeAndExpenses.Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }
        public IEnumerable<IncomeViewModel> Incomes { get; set; }
    }
}