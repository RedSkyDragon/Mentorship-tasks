using System;
using System.Web.Helpers;

namespace IncomeAndExpenses.BusinessLogic.Models
{
    /// <summary>
    /// BL model for filtration
    /// </summary>
    public class FilterBLModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets min date.
        /// </summary>
        public DateTime MinDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets max date.
        /// </summary>
        public DateTime MaxDate { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Gets or sets min amount.
        /// </summary>
        public decimal MinAmount { get; set; } = 0m;

        /// <summary>
        /// Gets or sets max amount.
        /// </summary>
        public decimal MaxAmount { get; set; } = 99999999.99m;

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the sorting column.
        /// </summary>
        public string SortCol { get; set; } = nameof(ExpenseBLModel.Date);

        /// <summary>
        /// Gets or sets the sorting direction.
        /// </summary>
        public SortDirection SortDir { get; set; } = SortDirection.Descending;

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
