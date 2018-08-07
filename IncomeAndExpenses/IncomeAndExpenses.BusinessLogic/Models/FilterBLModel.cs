using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace IncomeAndExpenses.BusinessLogic.Models
{
    public class FilterBLModel
    {
        public string UserId { get; set; } = string.Empty;

        public DateTime FromDate { get; set; } = DateTime.MinValue;

        public DateTime ToDate { get; set; } = DateTime.MaxValue;

        public decimal FromAmount { get; set; } = 0m;

        public decimal ToAmount { get; set; } = 99999999.99m;

        public string TypeName { get; set; } = string.Empty;

        public string SortCol { get; set; } = nameof(ExpenseBLModel.Date);

        public SortDirection SortDir { get; set; } = SortDirection.Descending;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
