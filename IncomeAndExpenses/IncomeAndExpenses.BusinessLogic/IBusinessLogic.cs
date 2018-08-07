using IncomeAndExpenses.BusinessLogic.Models;
using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Web.Helpers;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Interface to provide business logic
    /// </summary>
    public interface IBusinessLogic: IDisposable
    {
        /// <summary>
        /// Creates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        void CreateExpense(Expense expense);

        /// <summary>
        /// Creates income
        /// </summary>
        /// <param name="income">the income</param>
        void CreateIncome(Income income);

        /// <summary>
        /// Create expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        void CreateExpenseType(ExpenseType type);

        /// <summary>
        /// Creates income type
        /// </summary>
        /// <param name="type">the income type</param>
        void CreateIncomeType(IncomeType type);

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user">the user</param>
        void CreateUser(User user);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">the user</param>
        void UpdateUser(User user);

        /// <summary>
        /// Updates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        void UpdateExpense(Expense expense);

        /// <summary>
        /// Updates income
        /// </summary>
        /// <param name="income">the income</param>
        void UpdateIncome(Income income);

        /// <summary>
        /// Updates expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        void UpdateExpenseType(ExpenseType type);

        /// <summary>
        /// Updates income type
        /// </summary>
        /// <param name="type">the income type</param>
        void UpdateIncomeType(IncomeType type);

        /// <summary>
        /// Deletes expense
        /// </summary>
        /// <param name="id">Id of the expense</param>
        void DeleteExpense(int id);

        /// <summary>
        /// Deletes income
        /// </summary>
        /// <param name="id">Id of the income</param>
        void DeleteIncome(int id);

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
        /// Deletes income type with incomes
        /// </summary>
        /// <param name="id">Id of the income type</param>
        void DeleteIncomeType(int id);

        /// <summary>
        /// Deletes income type and replaces incomes' type
        /// </summary>
        /// <param name="id">Id of the income type</param>
        /// <param name="replaceId">Id of the replacement type</param>
        void DeleteAndReplaceIncomeType(int id, int replaceId);

        /// <summary>
        /// Gets expense with requested Id
        /// </summary>
        /// <param name="id">Id of the expense</param>
        /// <returns>Expense</returns>
        Expense GetExpense(int id);

        /// <summary>
        /// Gets income with requested Id
        /// </summary>
        /// <param name="id">Id of the income</param>
        /// <returns>Income</returns>
        Income GetIncome(int id);

        /// <summary>
        /// Gets expense type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>Expense type</returns>
        ExpenseType GetExpenseType(int id);

        /// <summary>
        /// Gets income type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>Income type</returns>
        IncomeType GetIncomeType(int id);

        /// <summary>
        /// Gets all expenses using requested filters
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <param name="pageSize">size of the page</param>
        /// <param name="pageNumber">current page number</param>
        /// <param name="searchValue">the search value</param>
        /// <param name="sortCol">name of the sorting column</param>
        /// <param name="sortDir">name of the sorting direction</param>
        /// <returns>Filled ExpensesBLModel</returns>
        ExpensesBLModel GetAllExpenses(FilterBLModel filter);

        /// <summary>
        /// Gets all expense types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>IEnumerable of expense types</returns>
        IEnumerable<ExpenseType> GetAllExpenseTypes(string userId);

        /// <summary>
        /// Gets all incomes using requested filters
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <param name="pageSize">size of the page</param>
        /// <param name="pageNumber">current page number</param>
        /// <param name="searchValue">the search value</param>
        /// <param name="sortCol">name of the sorting column</param>
        /// <param name="sortDir">name of the sorting direction</param>
        /// <returns>Filled IncomesBLModel</returns>
        IncomesBLModel GetAllIncomes(FilterBLModel filter);

        /// <summary>
        /// Gets all income types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>IEnumerable of income types</returns>
        IEnumerable<IncomeType> GetAllIncomeTypes(string userId);

        /// <summary>
        /// Gets information about totals
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>Totals</returns>
        TotalsBLModel GetTotals(string userId);

        /// <summary>
        /// Gets User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User</returns>
        User GetUser(string id);
    }
}