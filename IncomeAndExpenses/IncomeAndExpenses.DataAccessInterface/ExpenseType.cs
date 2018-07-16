using System.Collections.Generic;

namespace IncomeAndExpenses.DataAccessInterface
{
    public class ExpenseType : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public ExpenseType()
        {
            Expenses = new List<Expense>();
        }
    }
}
