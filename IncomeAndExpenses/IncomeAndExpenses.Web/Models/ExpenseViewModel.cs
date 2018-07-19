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
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\d+($|(\,\d{0,2}))$", ErrorMessage = "Format: xx,xx or xx,x or xx")]
        [Range(0.01, 99999999.99, ErrorMessage = "Amount shuld be greater than 0 and less than 99999999.99")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        [Required(ErrorMessage = "Required")]
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
    }
}