using System.Linq;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for Home Index page
    /// </summary>
    public class HomeIndexViewModel
    {
        /// <summary>
        /// Expenses belonging to the current user
        /// </summary>
        public IQueryable<ExpenseViewModel> Expenses { get; set; }

        /// <summary>
        /// Incomes belonging to the current user
        /// </summary>
        public IQueryable<IncomeViewModel> Incomes { get; set; }

        /// <summary>
        /// Info about Expenses pagination
        /// </summary>
        public PageInfoViewModel ExpensesPageInfo { get; set; }

        /// <summary>
        /// Info about Incomes pagination
        /// </summary>
        public PageInfoViewModel IncomesPageInfo { get; set; }

        /// <summary>
        /// Info about Expenses sorting
        /// </summary>
        public SortInfoViewModel ExpensesSortInfo { get; set; }

        /// <summary>
        /// Info about Incomes sorting
        /// </summary>
        public SortInfoViewModel IncomesSortInfo { get; set; }

        /// <summary>
        /// Sum of the Incomes Amount
        /// </summary>
        public decimal IncomeTotal { get; set; }

        /// <summary>
        /// Sum of the Expenses amount
        /// </summary>
        public decimal ExpenseTotal { get; set; }

        /// <summary>
        /// Diffecence between IncomeTotal and ExpenseTotal
        /// </summary>
        public decimal CurrentBalance { get; set; }
    }
}