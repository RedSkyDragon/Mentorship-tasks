using IncomeAndExpenses.BusinessLogic.Models;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Interface to provide business logic for totals
    /// </summary>
    public interface ITotalsBL
    {
        /// <summary>
        /// Gets information about totals
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>Totals</returns>
        Totals GetTotals(string userId);
    }
}
