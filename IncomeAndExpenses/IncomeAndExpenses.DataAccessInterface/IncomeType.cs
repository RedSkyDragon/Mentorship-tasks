using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    public class IncomeType : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
        public IncomeType()
        {
            Incomes = new List<Income>();
        }
    }
}
