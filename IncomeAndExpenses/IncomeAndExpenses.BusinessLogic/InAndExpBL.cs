using System.Collections.Generic;
using System.Linq;
using IncomeAndExpenses.DataAccessInterface;
using System.Web.Helpers;
using IncomeAndExpenses.BusinessLogic.Models;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Implements IbusinessLogic interface
    /// </summary>
    /// <seealso cref="IncomeAndExpenses.BusinessLogic.IBusinessLogic" />
    public class InAndExpBL : IBusinessLogic
    {
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="InAndExpBL"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork.</param>
        public InAndExpBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        public void CreateExpense(Expense expense)
        {
            _unitOfWork.Repository<Expense>().Create(expense);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Create expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        public void CreateExpenseType(ExpenseType type)
        {
            _unitOfWork.Repository<ExpenseType>().Create(type);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Creates income
        /// </summary>
        /// <param name="income">the income</param>
        public void CreateIncome(Income income)
        {
            _unitOfWork.Repository<Income>().Create(income);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Creates income type
        /// </summary>
        /// <param name="type">the income type</param>
        public void CreateIncomeType(IncomeType type)
        {
            _unitOfWork.Repository<IncomeType>().Create(type);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes expense
        /// </summary>
        /// <param name="id">Id of the expense</param>
        public void DeleteExpense(int id)
        {
            _unitOfWork.Repository<Expense>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes expense type with expenses
        /// </summary>
        /// <param name="id">Id of the expense type</param>
        public void DeleteExpenseType(int id)
        {
            var expenses = _unitOfWork.Repository<Expense>().All().Where(ex => ex.ExpenseTypeId == id);
            foreach (var expense in expenses)
            {
                _unitOfWork.Repository<Expense>().Delete(expense.Id);
            }
            _unitOfWork.Repository<ExpenseType>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes expense type and replaces expenses' type
        /// </summary>
        /// <param name="id">Id of the expense type</param>
        /// <param name="replaceId">Id of the replacement type</param>
        public void DeleteAndReplaceExpenseType(int id, int replaceId)
        {
            var expenses = _unitOfWork.Repository<Expense>().All().Where(ex => ex.ExpenseTypeId == id);
            foreach (var expense in expenses)
            {
                var upd = new Expense { Id = expense.Id, Amount = expense.Amount, Comment = expense.Comment, Date = expense.Date, ExpenseTypeId = replaceId };
                _unitOfWork.Repository<Expense>().Update(upd);
            }
            _unitOfWork.Repository<ExpenseType>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes income type with incomes
        /// </summary>
        /// <param name="id">Id of the income type</param>
        public void DeleteIncomeType(int id)
        {
            var incomes = _unitOfWork.Repository<Income>().All().Where(ex => ex.IncomeTypeId == id);
            foreach (var income in incomes)
            {
                _unitOfWork.Repository<Income>().Delete(income.Id);
            }
            _unitOfWork.Repository<IncomeType>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes income type and replaces incomes' type
        /// </summary>
        /// <param name="id">Id of the income type</param>
        /// <param name="replaceId">Id of the replacement type</param>
        public void DeleteAndReplaceIncomeType(int id, int replaceId)
        {
            var incomes = _unitOfWork.Repository<Income>().All().Where(ex => ex.IncomeTypeId == id);
            foreach (var income in incomes)
            {
                var upd = new Income { Id = income.Id, Amount = income.Amount, Comment = income.Comment, Date = income.Date, IncomeTypeId = replaceId };
                _unitOfWork.Repository<Income>().Update(upd);
            }
            _unitOfWork.Repository<IncomeType>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes income
        /// </summary>
        /// <param name="id">Id of the income</param>
        public void DeleteIncome(int id)
        {
            _unitOfWork.Repository<Income>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Gets User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>
        /// User
        /// </returns>
        public User GetUser(string id)
        {
            return _unitOfWork.Repository<string, User>().Get(id);
        }

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user">the user</param>
        public void CreateUser(User user)
        {
            _unitOfWork.Repository<string, User>().Create(user);
            _unitOfWork.Repository<IncomeType>().Create(new IncomeType { UserId = user.Id, Name = "Other", Description = "Income that are difficult to classify as specific type." });
            _unitOfWork.Repository<ExpenseType>().Create(new ExpenseType { UserId = user.Id, Name = "Other", Description = "Expense that are difficult to classify as specific type." });
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">the user</param>
        public void UpdateUser(User user)
        {
            _unitOfWork.Repository<string, User>().Update(user);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Gets expense with requested Id
        /// </summary>
        /// <param name="id">Id of the expense</param>
        /// <returns>
        /// Expense
        /// </returns>
        public Expense GetExpense(int id)
        {
            return _unitOfWork.Repository<Expense>().Get(id);
        }

        /// <summary>
        /// Gets expense type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>
        /// Expense type
        /// </returns>
        public ExpenseType GetExpenseType(int id)
        {
            return _unitOfWork.Repository<ExpenseType>().Get(id);
        }

        /// <summary>
        /// Gets income with requested Id
        /// </summary>
        /// <param name="id">Id of the income</param>
        /// <returns>
        /// Income
        /// </returns>
        public Income GetIncome(int id)
        {
            return _unitOfWork.Repository<Income>().Get(id);
        }

        /// <summary>
        /// Gets income type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>
        /// Income type
        /// </returns>
        public IncomeType GetIncomeType(int id)
        {
            return _unitOfWork.Repository<IncomeType>().Get(id);
        }

        /// <summary>
        /// Gets all expenses using requested filters
        /// </summary>
        /// <param name="filter">FilterBLModel for filtration</param>
        /// <returns>
        /// Filled ExpensesBLModel
        /// </returns>
        public ExpensesBLModel GetAllExpenses(FilterBLModel filter)
        {
            var types = _unitOfWork.Repository<ExpenseType>().All();
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                types = types.Where(t => t.UserId == filter.UserId);
            }
            var expenses = types.Join(_unitOfWork.Repository<Expense>().All(), t => t.Id, e => e.ExpenseTypeId,
                     (t, e) => new ExpenseBLModel { Id = e.Id, Amount = e.Amount, Date = e.Date, ExpenseTypeName = t.Name });
            if (!string.IsNullOrEmpty(filter.TypeName))
            {
                expenses = expenses.Where(e => e.ExpenseTypeName.Contains(filter.TypeName));
            }
            expenses = expenses.Where(e => e.Amount >= filter.MinAmount && e.Amount <= filter.MaxAmount)
                .Where(e => e.Date >= filter.MinDate && e.Date <= filter.MaxDate);
            int count = expenses.Count();
            expenses = SortExpenseBLModel(expenses, filter.SortCol, filter.SortDir).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            return new ExpensesBLModel { Expenses = expenses, Count = count };
        }

        /// <summary>
        /// Gets all expense types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>
        /// IEnumerable of expense types
        /// </returns>
        public IEnumerable<ExpenseType> GetAllExpenseTypes(string userId)
        {
            return _unitOfWork.Repository<ExpenseType>().All().Where(t => t.UserId == userId).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Gets all incomes using requested filters
        /// </summary>
        /// <param name="filter">FilterBLModel for filtration</param>
        /// <returns>
        /// Filled IncomesBLModel
        /// </returns>
        public IncomesBLModel GetAllIncomes(FilterBLModel filter)
        {
            var types = _unitOfWork.Repository<IncomeType>().All();
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                types = types.Where(t => t.UserId == filter.UserId);
            }
            var incomes = types.Join(_unitOfWork.Repository<Income>().All(), t => t.Id, e => e.IncomeTypeId,
                     (t, e) => new IncomeBLModel { Id = e.Id, Amount = e.Amount, Date = e.Date, IncomeTypeName = t.Name });
            if (!string.IsNullOrEmpty(filter.TypeName))
            {
                incomes = incomes.Where(e => e.IncomeTypeName.Contains(filter.TypeName));
            }
            incomes = incomes.Where(e => e.Amount >= filter.MinAmount && e.Amount <= filter.MaxAmount)
                .Where(e => e.Date >= filter.MinDate && e.Date <= filter.MaxDate);
            int count = incomes.Count();
            incomes = SortIncomeBLModel(incomes, filter.SortCol, filter.SortDir).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            return new IncomesBLModel { Incomes = incomes, Count = count };
        }

        /// <summary>
        /// Gets all income types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>
        /// IEnumerable of income types
        /// </returns>
        public IEnumerable<IncomeType> GetAllIncomeTypes(string userId)
        {
            return _unitOfWork.Repository<IncomeType>().All().Where(t => t.UserId == userId).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Gets information about totals
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>
        /// Totals
        /// </returns>
        public TotalsBLModel GetTotals(string userId)
        {
            var incomeTotal = _unitOfWork.Repository<IncomeType>().All()
               .Where(t => t.UserId == userId)
               .Sum(t => t.Incomes.Sum(i => i.Amount));
            var expenseTotal = _unitOfWork.Repository<ExpenseType>().All()
                .Where(t => t.UserId == userId)
                .Sum(t => t.Expenses.Sum(e => e.Amount));
            var currentBalance = incomeTotal - expenseTotal;
            return new TotalsBLModel { IncomeTotal = incomeTotal, ExpenseTotal = expenseTotal, CurrentBalance = currentBalance };
        }

        /// <summary>
        /// Updates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        public void UpdateExpense(Expense expense)
        {
            _unitOfWork.Repository<Expense>().Update(expense);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        public void UpdateExpenseType(ExpenseType type)
        {
            _unitOfWork.Repository<ExpenseType>().Update(type);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates income
        /// </summary>
        /// <param name="income">the income</param>
        public void UpdateIncome(Income income)
        {
            _unitOfWork.Repository<Income>().Update(income);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates income type
        /// </summary>
        /// <param name="type">the income type</param>
        public void UpdateIncomeType(IncomeType type)
        {
            _unitOfWork.Repository<IncomeType>().Update(type);
            _unitOfWork.Save();
        }

        private IQueryable<ExpenseBLModel> SortExpenseBLModel(IQueryable<ExpenseBLModel> expenses, string colName, SortDirection sortDir)
        {
            var result = expenses;
            switch (colName)
            {
                case nameof(ExpenseBLModel.Amount):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(ExpenseBLModel.Date):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(ExpenseBLModel.ExpenseTypeName):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.ExpenseTypeName).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.ExpenseTypeName).ThenByDescending(r => r.Id);
                    }
                    break;
            }
            return result;
        }

        private IQueryable<IncomeBLModel> SortIncomeBLModel(IQueryable<IncomeBLModel> expenses, string colName, SortDirection sortDir)
        {
            var result = expenses;
            switch (colName)
            {
                case nameof(IncomeBLModel.Amount):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(IncomeBLModel.Date):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(IncomeBLModel.IncomeTypeName):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.IncomeTypeName).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.IncomeTypeName).ThenByDescending(r => r.Id);
                    }
                    break;
            }
            return result;
        }
    }
}