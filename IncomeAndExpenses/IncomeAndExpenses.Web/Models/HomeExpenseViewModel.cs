using System.Linq;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for partial expenses list
    /// </summary>
    public class HomeExpenseViewModel
    {
        /// <summary>
        /// Expenses belonging to the current user
        /// </summary>
        public IQueryable<ExpenseViewModel> Expenses { get; set; }

        /// <summary>
        /// Info about Expenses pagination
        /// </summary>
        public PageInfoViewModel PageInfo { get; set; }

        /// <summary>
        /// Info about Expenses sorting
        /// </summary>
        public SortInfoViewModel SortInfo { get; set; }

        /// <summary>
        /// Value for search
        /// </summary>
        public string SearchValue { get; set; }
    }
}