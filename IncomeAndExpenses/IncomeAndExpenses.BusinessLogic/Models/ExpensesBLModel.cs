using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic
{
    public class ExpensesBLModel
    {
        public IEnumerable<ExpenseBLModel> Expenses { get; set; }

        public int Count { get; set; }
    }
}