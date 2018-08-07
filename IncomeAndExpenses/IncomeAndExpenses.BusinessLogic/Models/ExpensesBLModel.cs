using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// BL model for expenses list
    /// </summary>
    public class ExpensesBLModel
    {
        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        public IEnumerable<ExpenseBLModel> Expenses { get; set; }

        /// <summary>
        /// Gets or sets the count for pagination.
        /// </summary>
        public int Count { get; set; }
    }
}