using System.Collections.Generic;

namespace IncomeAndExpenses.Models
{
    public class IncomeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsStandart { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Income> Incomes { get; set; }
        public IncomeType()
        {
            Incomes = new List<Income>();
        }
    }
}
