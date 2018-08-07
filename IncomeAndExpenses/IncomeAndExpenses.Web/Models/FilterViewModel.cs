using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    public class FilterViewModel
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal? FromAmount { get; set; }

        [DataType(DataType.Currency)]
        public decimal? ToAmount { get; set; }

        public string TypeName { get; set; }

        public string SortCol { get; set; } = nameof(ExpenseViewModel.Date);

        public SortDirection SortDir { get; set; } = SortDirection.Descending;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}