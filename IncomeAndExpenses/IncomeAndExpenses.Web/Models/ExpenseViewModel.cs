using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for Expense
    /// </summary>
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int ExpenseTypeId { get; set; }

        [DisplayName("Expense type")]
        public string ExpenseTypeName { get; set; }
    }
}