using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents Expense type entity
    /// </summary>
    public class ExpenseType : Entity<int>
    {
        /// <summary>
        /// Creates new expense type
        /// </summary>
        public ExpenseType()
        {
            Expenses = new List<Expense>();
        }

        /// <summary>
        /// Gets or sets name of the type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of the type
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets Id of user created this type
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets user of this type
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// ICollection of expenses with this type
        /// </summary>
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
