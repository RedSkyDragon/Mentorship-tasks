using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncomeAndExpenses.Web.Models
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Comment { get; set; }


        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
    }
}