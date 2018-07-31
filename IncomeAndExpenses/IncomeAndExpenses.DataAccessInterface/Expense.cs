using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents Expense entity
    /// </summary>
    public class Expense : Entity<int>
    {
        /// <summary>
        /// Gets or sets amount of expense
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets expense date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets comment about expense
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets Expense type id
        /// </summary>
        public int ExpenseTypeId { get; set; }

        /// <summary>
        /// Gets or sets Expense type
        /// </summary>
        public virtual ExpenseType ExpenseType { get; set; }
    }
}
