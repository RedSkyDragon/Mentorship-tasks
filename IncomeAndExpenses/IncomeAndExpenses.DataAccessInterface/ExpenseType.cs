using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents Expense type entity
    /// </summary>
    public class ExpenseTypeDM : EntityDM<int>
    {
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
        public virtual UserDM User { get; set; }

        /// <summary>
        /// ICollection of expenses with this type
        /// </summary>
        public virtual ICollection<ExpenseDM> Expenses { get; set; } = new List<ExpenseDM>();
    }
}
