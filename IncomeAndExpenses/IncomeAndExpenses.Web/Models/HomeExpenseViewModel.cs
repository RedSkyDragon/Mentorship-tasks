using System.Collections.Generic;
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
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }

        /// <summary>
        /// Info about Expenses pagination
        /// </summary>
        public PageInfoViewModel PageInfo { get; set; }

        /// <summary>
        /// Info about Expenses sorting
        /// </summary>
        public SortInfoViewModel SortInfo { get; set; }

        /// <summary>
        /// Filter options
        /// </summary>
        public FilterViewModel Filter { get; set; }
    }
}