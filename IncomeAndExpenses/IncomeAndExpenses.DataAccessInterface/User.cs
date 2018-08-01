using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents user of the application
    /// </summary>
    public class User : Entity<string>
    {
        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ICollection of users Expense types
        /// </summary>
        public virtual ICollection<ExpenseType> ExpenseTypes { get; set; } = new List<ExpenseType>();

        /// <summary>
        /// ICollection of users Income types
        /// </summary>
        public virtual ICollection<IncomeType> IncomeTypes { get; set; } = new List<IncomeType>();
    }
}
