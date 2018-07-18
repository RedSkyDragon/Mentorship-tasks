using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    public class DeleteIncomeTypeViewModel
    {
        public IncomeTypeViewModel IncomeType { get; set; }
        public IEnumerable<SelectListItem> ReplacementTypes { get; set; }

    }
}