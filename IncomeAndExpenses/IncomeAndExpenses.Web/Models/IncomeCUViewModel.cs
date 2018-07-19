using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    public class IncomeCUViewModel
    {
        public IncomeViewModel Income { get; set; }
        public IEnumerable<SelectListItem> IncomeTypes { get; set; }
    }
}