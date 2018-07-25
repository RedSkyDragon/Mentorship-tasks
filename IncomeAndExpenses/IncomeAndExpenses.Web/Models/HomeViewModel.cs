using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;
using System.Linq;

namespace IncomeAndExpenses.Web.Models
{
    public class HomeIndexViewModel
    {
        public IQueryable<ExpenseViewModel> Expenses { get; set; }

        public IQueryable<IncomeViewModel> Incomes { get; set; }

        public PageInfoViewModel ExpensesPageInfo { get; set; }

        public PageInfoViewModel IncomesPageInfo { get; set; }

        public SortInfoViewModel ExpensesSortInfo { get; set; }

        public SortInfoViewModel IncomesSortInfo { get; set; }

        public decimal IncomeTotal { get; set; }

        public decimal ExpenseTotal { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}