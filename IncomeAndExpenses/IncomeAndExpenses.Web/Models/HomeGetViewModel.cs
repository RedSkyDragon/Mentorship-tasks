using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Models
{
    public class HomeGetViewModel
    {
        public int IncomePage { get; set; } = 1;

        public int ExpensePage { get; set; } = 1;

        public string IncomeSortCol { get; set; } = nameof(IncomeViewModel.Date);

        public string ExpenseSortCol { get; set; } = nameof(ExpenseViewModel.Date);

        public SortDirection IncomeSortDir { get; set; } = SortDirection.Descending;

        public SortDirection ExpenseSortDir { get; set; } = SortDirection.Descending;
    }
}