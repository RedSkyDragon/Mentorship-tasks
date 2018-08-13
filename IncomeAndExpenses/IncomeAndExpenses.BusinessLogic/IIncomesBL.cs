using IncomeAndExpenses.BusinessLogic.Models;
using IncomeAndExpenses.DataAccessInterface;
using System;
using System.Collections.Generic;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Interface to provide business logic for incomes
    /// </summary>
    public interface IIncomesBL
    {
        /// <summary>
        /// Creates income
        /// </summary>
        /// <param name="income">the income</param>
        void CreateIncome(IncomeDM income);

        /// <summary>
        /// Creates income type
        /// </summary>
        /// <param name="type">the income type</param>
        void CreateIncomeType(IncomeTypeDM type);

        /// <summary>
        /// Updates income
        /// </summary>
        /// <param name="income">the income</param>
        void UpdateIncome(IncomeDM income);

        /// <summary>
        /// Updates income type
        /// </summary>
        /// <param name="type">the income type</param>
        void UpdateIncomeType(IncomeTypeDM type);

        /// <summary>
        /// Deletes income
        /// </summary>
        /// <param name="id">Id of the income</param>
        void DeleteIncome(int id);

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
        /// Gets income with requested Id
        /// </summary>
        /// <param name="id">Id of the income</param>
        /// <returns>Income</returns>
        IncomeDM GetIncome(int id);

        /// <summary>
        /// Gets income type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>Income type</returns>
        IncomeTypeDM GetIncomeType(int id);

        /// <summary>
        /// Gets all incomes using requested filters
        /// </summary>
        /// <param name="filter">FilterBLModel for filtration</param>
        /// <returns>Filled IncomesBLModel</returns>
        Incomes GetAllIncomes(FilterBL filter);

        /// <summary>
        /// Gets all income types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>IEnumerable of income types</returns>
        IEnumerable<IncomeTypeDM> GetAllIncomeTypes(string userId);

    }
}