using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic.Models
{
    /// <summary>
    /// BL model for expenses list
    /// </summary>
    public class Expenses
    {
        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        public IEnumerable<Expense> ExpensesList { get; set; }

        /// <summary>
        /// Gets or sets the total count for pagination.
        /// </summary>
        public int TotalCount { get; set; }
    }
}