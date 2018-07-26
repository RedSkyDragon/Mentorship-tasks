using System.Collections.Generic;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    /// <summary>
    /// Model validator for Expense View
    /// </summary>
    internal class ExpenseViewValidator : ModelValidator
    {
        /// <summary>
        /// Creates ExpenseViewValidator
        /// </summary>
        /// <param name="metadata">The metadata</param>
        /// <param name="controllerContext">The controller context</param>
        public ExpenseViewValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext) { }

        /// <summary>
        /// Validates the object
        /// </summary>
        /// <param name="container">The container</param>
        /// <returns>A collection of validation results.</returns>
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {            
            return new List<ModelValidationResult>();
        }
    }
}