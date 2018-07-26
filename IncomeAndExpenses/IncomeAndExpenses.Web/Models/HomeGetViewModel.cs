using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for GET requests at HomeController
    /// </summary>
    public class HomeGetViewModel
    {
        /// <summary>
        /// Current incomes page
        /// </summary>
        public int IncomePage { get; set; } = 1;

        /// <summary>
        /// Current expenses page
        /// </summary>
        public int ExpensePage { get; set; } = 1;

        /// <summary>
        /// Incomes sorting column
        /// </summary>
        public string IncomeSortCol { get; set; } = nameof(IncomeViewModel.Date);

        /// <summary>
        /// Expenses sorting column
        /// </summary>
        public string ExpenseSortCol { get; set; } = nameof(ExpenseViewModel.Date);

        /// <summary>
        /// Incomes sorting direction
        /// </summary>
        public SortDirection IncomeSortDir { get; set; } = SortDirection.Descending;

        /// <summary>
        /// Expenses sorting direction
        /// </summary>
        public SortDirection ExpenseSortDir { get; set; } = SortDirection.Descending;
    }
}