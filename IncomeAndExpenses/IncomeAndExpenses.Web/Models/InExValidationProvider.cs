using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Models
{
    public class InExValidationProvider: ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata.ContainerType == typeof(ExpenseViewModel))
            {
                return new ModelValidator[] { new ExpenseViewPropertyValidator(metadata, context) };
            }

            if (metadata.ModelType == typeof(ExpenseViewModel))
            {
                return new ModelValidator[] { new ExpenseViewValidator(metadata, context) };
            }

            return Enumerable.Empty<ModelValidator>();
        }
    }
}