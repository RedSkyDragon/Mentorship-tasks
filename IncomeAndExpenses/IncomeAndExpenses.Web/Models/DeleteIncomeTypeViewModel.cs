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

    }
}