using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents Income entity
    /// </summary>
    public class Income : Entity<int>
    {
        /// <summary>
        /// Gets or sets amount of income
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets income date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets comment about income
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets income type Id
        /// </summary>
        public int IncomeTypeId { get; set; }

        /// <summary>
        /// Gets or sets Income type
        /// </summary>
        public virtual IncomeType IncomeType { get; set; }
    }
}
