using System;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// BL model for expense
    /// </summary>
    public class ExpenseBLModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the expense type identifier.
        /// </summary>
        public int ExpenseTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the expense type.
        /// </summary>
        public string ExpenseTypeName { get; set; }
    }
}