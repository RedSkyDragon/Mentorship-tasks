using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic.Models
{
    /// <summary>
    /// Bl model for income list
    /// </summary>
    public class Incomes
    {
        /// <summary>
        /// Gets or sets the incomes.
        /// </summary>
        public IEnumerable<Income> IncomesList { get; set; }

        /// <summary>
        /// Gets or sets the total count for pagination.
        /// </summary>
        public int TotalCount { get; set; }
    }
}