using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Bl model for income list
    /// </summary>
    public class IncomesBLModel
    {
        /// <summary>
        /// Gets or sets the incomes.
        /// </summary>
        public IEnumerable<IncomeBLModel> Incomes { get; set; }

        /// <summary>
        /// Gets or sets the count for pagination.
        /// </summary>
        public int Count { get; set; }
    }
}