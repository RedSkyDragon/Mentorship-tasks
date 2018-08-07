using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IncomeAndExpenses.Web.Models
{
    /// <summary>
    /// ViewModel for Income
    /// </summary>
    public class IncomeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\d+($|(\.\d{0,2}))$", ErrorMessage = "Format: xx,xx or xx,x or xx")]
        [Range(0.01, 99999999.99, ErrorMessage = "Amount shuld be greater than 0 and less than 99999999,99")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        [Required(ErrorMessage = "Required")]
        public int IncomeTypeId { get; set; }

        [DisplayName("Income type")]
        public string IncomeTypeName { get; set; }
    }
}