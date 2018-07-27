using System.Linq;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for partial incomes list
    /// </summary>
    public class HomeIncomeViewModel
    {
        /// <summary>
        /// Incomes belonging to the current user
        /// </summary>
        public IQueryable<IncomeViewModel> Incomes { get; set; }

        /// <summary>
        /// Info about Incomes pagination
        /// </summary>
        public PageInfoViewModel PageInfo { get; set; }

        /// <summary>
        /// Info about Incomes sorting
        /// </summary>
        public SortInfoViewModel SortInfo { get; set; }

        /// <summary>
        /// Value for search
        /// </summary>
        public string SearchValue { get; set; }
    }
}