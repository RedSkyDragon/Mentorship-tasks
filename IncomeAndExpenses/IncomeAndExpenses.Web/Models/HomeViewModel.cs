using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;

namespace IncomeAndExpenses.Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }

        public IEnumerable<IncomeViewModel> Incomes { get; set; }

        public PageInfoViewModel ExpensesPageInfo { get; set; }

        public PageInfoViewModel IncomesPageInfo { get; set; }

        public SortInfoViewModel ExpensesSortInfo { get; set; }

        public SortInfoViewModel IncomeSortInfo { get; set; }

        public decimal IncomeTotal { get; set; }

        public decimal ExpenseTotal { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}