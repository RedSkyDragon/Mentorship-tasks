using System.Collections.Generic;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for delete income type page
    /// </summary>
    public class DeleteIncomeTypeViewModel
    {
        /// <summary>
        /// IncomeType to delete
        /// </summary>
        public IncomeTypeViewModel IncomeType { get; set; }

        /// <summary>
        /// IncomeTypes belonging to the same user which could replace deleted type
        /// </summary>
        public IEnumerable<SelectListItem> ReplacementTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether need to delete all incomes with type.
        /// </summary>
        public bool DeleteAll { get; set; }

        /// <summary>
        /// Gets or sets the replacement type identifier.
        /// </summary>
        public int? ReplacementTypeId { get; set; }
    }
}