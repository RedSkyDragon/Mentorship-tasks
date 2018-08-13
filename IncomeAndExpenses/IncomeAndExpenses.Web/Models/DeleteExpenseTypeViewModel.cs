using System.Collections.Generic;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for delete expense type page
    /// </summary>
    public class DeleteExpenseTypeViewModel
    {
        /// <summary>
        /// Expense type to delete
        /// </summary>
        public ExpenseTypeViewModel ExpenseType { get; set; }

        /// <summary>
        /// ExpenseTypes belonging to the same user which could replace deleted type
        /// </summary>
        public IEnumerable<SelectListItem> ReplacementTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether need to delete all expenses with type.
        /// </summary>
        public bool DeleteAll { get; set; }

        /// <summary>
        /// Gets or sets the replacement type identifier.
        /// </summary>
        public int? ReplacementTypeId { get; set; }
    }
}