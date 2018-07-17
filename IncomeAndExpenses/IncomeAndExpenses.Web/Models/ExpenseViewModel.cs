using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    public class ExpenseViewModel
    {
        public Expense Expense { get; set; }
        public IEnumerable<SelectListItem> ExpenseTypes { get; set; }
        public Income Income { get; internal set; }
    }
}