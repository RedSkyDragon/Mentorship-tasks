using IncomeAndExpenses.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    /// <summary>
    /// Validation provider (working with ExpenseViewModel)
    /// </summary>
    public class InExValidationProvider: ModelValidatorProvider
    {
        /// <summary>
        /// Gets a list of validators
        /// </summary>
        /// <param name="metadata">The metadata</param>
        /// <param name="context">The context</param>
        /// <returns>List of validators</returns>
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