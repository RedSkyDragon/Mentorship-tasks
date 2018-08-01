using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    /// <summary>
    /// Represents IncomeType entity
    /// </summary>
    public class IncomeType : Entity<int>
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
        /// Gets or sets User of this type
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// ICollection of incomes with this type
        /// </summary>
        public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
    }
}
