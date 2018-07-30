using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for information about sorting
    /// </summary>
    public class SortInfoViewModel
    {
        /// <summary>
        /// Name of the sorting column
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Sorting direction
        /// </summary>
        public SortDirection Direction { get; set; }
    }
}