using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeAndExpenses.Models
{
    public class Income
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}
