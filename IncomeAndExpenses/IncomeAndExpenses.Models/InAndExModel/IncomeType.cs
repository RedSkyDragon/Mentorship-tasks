using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeAndExpenses.Models
{
    public class IncomeType
    {
        public int Id { get; set; }
        [Required]
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
