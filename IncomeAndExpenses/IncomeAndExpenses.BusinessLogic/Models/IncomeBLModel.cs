using System;

namespace IncomeAndExpenses.BusinessLogic
{
    public class IncomeBLModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int IncomeTypeId { get; set; }

        public string IncomeTypeName { get; set; }
    }
}