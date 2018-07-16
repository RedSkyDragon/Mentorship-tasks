using System.Collections.Generic;

namespace IncomeAndExpenses.Models
{
    public class User 
    {
        public User()
        {
            ExpenseTypes = new List<ExpenseType>();
            IncomeTypes = new List<IncomeType>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }

        public ICollection<ExpenseType> ExpenseTypes { get; set; }
        public ICollection<IncomeType> IncomeTypes { get; set; }            
    }
}
