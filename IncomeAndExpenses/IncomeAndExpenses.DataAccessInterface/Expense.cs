using System;
using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.DataAccessInterface
{
    public class Expense : Entity<int>
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int ExpenseTypeId { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
    }
}
