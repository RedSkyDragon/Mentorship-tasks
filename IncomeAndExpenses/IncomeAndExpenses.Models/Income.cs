using System;

namespace IncomeAndExpenses.Models
{
    public class Income
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}
