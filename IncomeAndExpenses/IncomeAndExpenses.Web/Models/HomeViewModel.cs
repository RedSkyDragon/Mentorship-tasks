namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for Home Index page
    /// </summary>
    public class HomeIndexViewModel
    {
        /// <summary>
        /// Sum of the Incomes Amount
        /// </summary>
        public decimal IncomeTotal { get; set; }

        /// <summary>
        /// Sum of the Expenses amount
        /// </summary>
        public decimal ExpenseTotal { get; set; }

        /// <summary>
        /// Diffecence between IncomeTotal and ExpenseTotal
        /// </summary>
        public decimal CurrentBalance { get; set; }
    }
}