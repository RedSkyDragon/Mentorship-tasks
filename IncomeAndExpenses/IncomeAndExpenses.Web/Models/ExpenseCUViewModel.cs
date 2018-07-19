using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    public class ExpenseCUViewModel
    {
        public ExpenseViewModel Expense { get; set; }
        public IEnumerable<SelectListItem> ExpenseTypes { get; set; }
    }
}