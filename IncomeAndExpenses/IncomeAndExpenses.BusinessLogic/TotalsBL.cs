using IncomeAndExpenses.BusinessLogic.Models;
using IncomeAndExpenses.DataAccessInterface;
using System.Linq;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Implements ITotalsBL interface
    /// </summary>
    public class TotalsBL: ITotalsBL
    {
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork.</param>
        public TotalsBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets information about totals
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>
        /// Totals
        /// </returns>
        public Totals GetTotals(string userId)
        {
            decimal incomeTotal = _unitOfWork.Repository<IncomeTypeDM>().All()
               .Where(t => t.UserId == userId)
               .Sum(t => t.Incomes.Sum(e => (decimal?)e.Amount)) ?? 0m;
            decimal expenseTotal = _unitOfWork.Repository<ExpenseTypeDM>().All()
                .Where(t => t.UserId == userId)
                .Sum(t => t.Expenses.Sum(e => (decimal?)e.Amount)) ?? 0m;
            var currentBalance = incomeTotal - expenseTotal;
            return new Totals { IncomeTotal = incomeTotal, ExpenseTotal = expenseTotal};
        }
    }
}
