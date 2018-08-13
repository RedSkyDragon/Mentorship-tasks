using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents user of the application
    /// </summary>
    public class UserDM : EntityDM<string>
    {
        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ICollection of users Expense types
        /// </summary>
        public virtual ICollection<ExpenseTypeDM> ExpenseTypes { get; set; } = new List<ExpenseTypeDM>();

        /// <summary>
        /// ICollection of users Income types
        /// </summary>
        public virtual ICollection<IncomeTypeDM> IncomeTypes { get; set; } = new List<IncomeTypeDM>();
    }
}
