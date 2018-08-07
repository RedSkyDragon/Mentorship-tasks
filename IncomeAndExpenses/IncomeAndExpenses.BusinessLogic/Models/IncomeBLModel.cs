using System;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// BL model for income
    /// </summary>
    public class IncomeBLModel
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
        /// Gets or sets the income type identifier.
        /// </summary>
        public int IncomeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the income type.
        /// </summary>
        public string IncomeTypeName { get; set; }
    }
}