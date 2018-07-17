using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.DataAccessInterface
{
    public class Income : Entity<int>
    {
        public decimal Amount { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int IncomeTypeId { get; set; }
        public virtual IncomeType IncomeType { get; set; }
    }
}
