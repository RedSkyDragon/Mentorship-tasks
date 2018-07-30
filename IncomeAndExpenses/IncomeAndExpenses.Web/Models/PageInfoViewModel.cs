using System;
using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for information about pagination
    /// </summary>
    public class PageInfoViewModel
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Size of one page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Number of all items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Number of pages
        /// </summary>
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); } }
    }
}