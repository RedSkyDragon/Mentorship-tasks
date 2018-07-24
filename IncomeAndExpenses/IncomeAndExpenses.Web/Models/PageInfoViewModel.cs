using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomeAndExpenses.Web.Models
{
    public class PageInfoViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}