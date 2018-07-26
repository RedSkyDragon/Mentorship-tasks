using System.Collections.Generic;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for create and edit expense page
    /// </summary>
    public class ExpenseCUViewModel
    {
        /// <summary>
        /// Expense to create or edit
        /// </summary>
        public ExpenseViewModel Expense { get; set; }
        
        /// <summary>
        /// ExpenseTypes blonging to the current user
        /// </summary>
        public IEnumerable<SelectListItem> ExpenseTypes { get; set; }
    }
}