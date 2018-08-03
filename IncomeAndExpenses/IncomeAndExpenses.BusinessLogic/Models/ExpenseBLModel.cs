using System;

namespace IncomeAndExpenses.BusinessLogic
{
    public class ExpenseBLModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int ExpenseTypeId { get; set; }

        public string ExpenseTypeName { get; set; }
    }
}