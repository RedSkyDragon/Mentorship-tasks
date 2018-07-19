using System.Collections.Generic;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    internal class ExpenseViewValidator : ModelValidator
    {
        public ExpenseViewValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext) { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {            
            return new List<ModelValidationResult>();
        }
    }
}