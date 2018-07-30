using System.Collections.Generic;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for create and edit income page
    /// </summary>
    public class IncomeCUViewModel
    {
        /// <summary>
        /// Income to create or edit
        /// </summary>
        public IncomeViewModel Income { get; set; }

        /// <summary>
        /// IncomeTypes blonging to the current user
        /// </summary>
        public IEnumerable<SelectListItem> IncomeTypes { get; set; }
    }
}