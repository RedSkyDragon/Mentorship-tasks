using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Models
{
    public class SortInfoViewModel
    {
        public string ColumnName { get; set; }

        public SortDirection Direction { get; set; }
    }
}