using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic
{
    public class IncomesBLModel
    {
        public IEnumerable<IncomeBLModel> Incomes { get; set; }

        public int Count { get; set; }
    }
}