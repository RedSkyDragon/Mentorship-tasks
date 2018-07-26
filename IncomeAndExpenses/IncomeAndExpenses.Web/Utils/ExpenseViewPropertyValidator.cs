using IncomeAndExpenses.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    /// <summary>
    /// Volidator for ExpenseView properties
    /// </summary>
    internal class ExpenseViewPropertyValidator : ModelValidator
    {
        /// <summary>
        /// Creates ExpenseView property validator
        /// </summary>
        /// <param name="metadata">the metadata</param>
        /// <param name="controllerContext">thr context</param>
        public ExpenseViewPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext) { }

        /// <summary>
        /// Validates property
        /// </summary>
        /// <param name="container">The container</param>
        /// <returns>List of validation results</returns>
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (container is ExpenseViewModel expense)
            {
                switch (Metadata.PropertyName)
                {
                    case nameof(ExpenseViewModel.Amount):
                        if (expense.Amount < 0.01m || expense.Amount > 99999999.99m)
                        {
                            return new ModelValidationResult[]{ new ModelValidationResult { MemberName= "", Message="Amount shuld be greater than 0 and less than 99999999,99"}};
                        }
                        break;
                    case nameof(ExpenseViewModel.Date):
                        if (expense.Date == null)
                        {
                            return new ModelValidationResult[]{new ModelValidationResult { MemberName="", Message="Date required"}};
                        }
                        if (expense.Date > DateTime.Today)
                        {
                            return new ModelValidationResult[] { new ModelValidationResult { MemberName = "", Message = "Should be less or equal than today" } };
                        }
                        break;
                }
            }
            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}