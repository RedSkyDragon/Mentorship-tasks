using IncomeAndExpenses.BusinessLogic.Models;
using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Interface to provide business logic for expenses
    /// </summary>
    public interface IExpensesBL
    {
        /// <summary>
        /// Creates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        void CreateExpense(ExpenseDM expense);

        /// <summary>
        /// Create expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        void CreateExpenseType(ExpenseTypeDM type);

        /// <summary>
        /// Updates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        void UpdateExpense(ExpenseDM expense);

        /// <summary>
        /// Updates expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        void UpdateExpenseType(ExpenseTypeDM type);

        /// <summary>
        /// Deletes expense
        /// </summary>
        /// <param name="id">Id of the expense</param>
        void DeleteExpense(int id);

        /// <summary>
        /// Deletes expense type with expenses
        /// </summary>
        /// <param name="id">Id of the expense type</param>
        void DeleteExpenseType(int id);

        /// <summary>
        /// Deletes expense type and replaces expenses' type
        /// </summary>
        /// <param name="id">Id of the expense type</param>
        /// <param name="replaceId">Id of the replacement type</param>
        void DeleteAndReplaceExpenseType(int id, int replaceId);

        /// <summary>
        /// Gets expense with requested Id
        /// </summary>
        /// <param name="id">Id of the expense</param>
        /// <returns>Expense</returns>
        ExpenseDM GetExpense(int id);

        /// <summary>
        /// Gets expense type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>Expense type</returns>
        ExpenseTypeDM GetExpenseType(int id);

        /// <summary>
        /// Gets all expenses using requested filters
        /// </summary>
        /// <param name="filter">FilterBLModel for filtration</param>
        /// <returns>Filled ExpensesBLModel</returns>
        Expenses GetAllExpenses(FilterBL filter);

        /// <summary>
        /// Gets all expense types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>IEnumerable of expense types</returns>
        IEnumerable<ExpenseTypeDM> GetAllExpenseTypes(string userId);

    }
}
