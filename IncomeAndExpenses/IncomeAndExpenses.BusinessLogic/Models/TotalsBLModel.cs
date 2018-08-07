namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// BL model for totals
    /// </summary>
    public class TotalsBLModel
    {
        /// <summary>
        /// Gets or sets the income total.
        /// </summary>
        public decimal IncomeTotal { get; set; }

        /// <summary>
        /// Gets or sets the expense total.
        /// </summary>
        public decimal ExpenseTotal { get; set; }

        /// <summary>
        /// Gets or sets the current balance.
        /// </summary>
        public decimal CurrentBalance { get; set; }
    }
}