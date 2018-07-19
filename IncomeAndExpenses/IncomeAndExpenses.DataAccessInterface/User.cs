using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    public class User : Entity<string>
    {
        public User()
        {
            ExpenseTypes = new List<ExpenseType>();
            IncomeTypes = new List<IncomeType>();
        }

        public string UserName { get; set; }

        public virtual ICollection<ExpenseType> ExpenseTypes { get; set; }
        public virtual ICollection<IncomeType> IncomeTypes { get; set; }            
    }
}
