using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// View model for income or expenses filtration
    /// </summary>
    public class FilterViewModel
    {
        /// <summary>
        /// Gets or sets min date.
        /// </summary>
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? MinDate { get; set; }

        /// <summary>
        /// Gets or sets max date.
        /// </summary>
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? MaxDate { get; set; }

        /// <summary>
        /// Gets or sets min amount.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? MinAmount { get; set; }

        /// <summary>
        /// Gets or sets max amount.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the sorting column.
        /// </summary>
        public string SortCol { get; set; } = nameof(ExpenseViewModel.Date);

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