using IncomeAndExpenses.BusinessLogic.Models;
using IncomeAndExpenses.DataAccessInterface;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace IncomeAndExpenses.BusinessLogic
{
    /// <summary>
    /// Implements IExpensesBL interface
    /// </summary>
    public class ExpensesBL: IExpensesBL
    {
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork.</param>
        public ExpensesBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        public void CreateExpense(ExpenseDM expense)
        {
            _unitOfWork.Repository<ExpenseDM>().Create(expense);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Create expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        public void CreateExpenseType(ExpenseTypeDM type)
        {
            _unitOfWork.Repository<ExpenseTypeDM>().Create(type);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes expense
        /// </summary>
        /// <param name="id">Id of the expense</param>
        public void DeleteExpense(int id)
        {
            _unitOfWork.Repository<ExpenseDM>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes expense type with expenses
        /// </summary>
        /// <param name="id">Id of the expense type</param>
        public void DeleteExpenseType(int id)
        {
            var expenses = _unitOfWork.Repository<ExpenseDM>().All().Where(ex => ex.ExpenseTypeId == id);
            foreach (var expense in expenses)
            {
                _unitOfWork.Repository<ExpenseDM>().Delete(expense.Id);
            }
            _unitOfWork.Repository<ExpenseTypeDM>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Deletes expense type and replaces expenses' type
        /// </summary>
        /// <param name="id">Id of the expense type</param>
        /// <param name="replaceId">Id of the replacement type</param>
        public void DeleteAndReplaceExpenseType(int id, int replaceId)
        {
            var expenses = _unitOfWork.Repository<ExpenseDM>().All().Where(ex => ex.ExpenseTypeId == id);
            foreach (var expense in expenses)
            {
                var upd = new ExpenseDM { Id = expense.Id, Amount = expense.Amount, Comment = expense.Comment, Date = expense.Date, ExpenseTypeId = replaceId };
                _unitOfWork.Repository<ExpenseDM>().Update(upd);
            }
            _unitOfWork.Repository<ExpenseTypeDM>().Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Gets expense with requested Id
        /// </summary>
        /// <param name="id">Id of the expense</param>
        /// <returns>
        /// Expense
        /// </returns>
        public ExpenseDM GetExpense(int id)
        {
            return _unitOfWork.Repository<ExpenseDM>().Get(id);
        }

        /// <summary>
        /// Gets expense type with requested Id
        /// </summary>
        /// <param name="id">Id of the type</param>
        /// <returns>
        /// Expense type
        /// </returns>
        public ExpenseTypeDM GetExpenseType(int id)
        {
            return _unitOfWork.Repository<ExpenseTypeDM>().Get(id);
        }

        /// <summary>
        /// Gets all expenses using requested filters
        /// </summary>
        /// <param name="filter">FilterBLModel for filtration</param>
        /// <returns>
        /// Filled ExpensesBLModel
        /// </returns>
        public Expenses GetAllExpenses(FilterBL filter)
        {
            var types = _unitOfWork.Repository<ExpenseTypeDM>().All();
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                types = types.Where(t => t.UserId == filter.UserId);
            }
            var expenses = types.Join(_unitOfWork.Repository<ExpenseDM>().All(), t => t.Id, e => e.ExpenseTypeId,
                     (t, e) => new Expense { Id = e.Id, Amount = e.Amount, Date = e.Date, TypeName = t.Name });
            if (!string.IsNullOrEmpty(filter.TypeName))
            {
                expenses = expenses.Where(e => e.TypeName.Contains(filter.TypeName));
            }
            if (filter.MinAmount.HasValue)
            {
                expenses = expenses.Where(e => e.Amount >= filter.MinAmount.Value);
            }
            if (filter.MaxAmount.HasValue)
            {
                expenses = expenses.Where(e => e.Amount <= filter.MaxAmount.Value);
            }
            if (filter.MinDate.HasValue)
            {
                expenses = expenses.Where(e => e.Date >= filter.MinDate.Value);
            }
            if (filter.MaxDate.HasValue)
            {
                expenses = expenses.Where(e => e.Date <= filter.MaxDate.Value);
            }
            int count = expenses.Count();
            expenses = SortExpenseBLModel(expenses, filter.SortCol, filter.SortDir).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            return new Expenses { ExpensesList = expenses, TotalCount = count };
        }

        /// <summary>
        /// Gets all expense types for current user
        /// </summary>
        /// <param name="userId">current user Id</param>
        /// <returns>
        /// IEnumerable of expense types
        /// </returns>
        public IEnumerable<ExpenseTypeDM> GetAllExpenseTypes(string userId)
        {
            return _unitOfWork.Repository<ExpenseTypeDM>().All().Where(t => t.UserId == userId).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Updates expense
        /// </summary>
        /// <param name="expense">the expense</param>
        public void UpdateExpense(ExpenseDM expense)
        {
            _unitOfWork.Repository<ExpenseDM>().Update(expense);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Updates expense type
        /// </summary>
        /// <param name="type">the expense type</param>
        public void UpdateExpenseType(ExpenseTypeDM type)
        {
            _unitOfWork.Repository<ExpenseTypeDM>().Update(type);
            _unitOfWork.Save();
        }

        private IQueryable<Expense> SortExpenseBLModel(IQueryable<Expense> expenses, string colName, SortDirection sortDir)
        {
            var result = expenses as IOrderedQueryable<Expense>;
            switch (colName)
            {
                case nameof(Expense.Amount):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Amount);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Amount);
                    }
                    break;
                case nameof(Expense.Date):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Date);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Date);
                    }
                    break;
                case nameof(Expense.TypeName):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.TypeName);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.TypeName);
                    }
                    break;
                default:
                    result = result.OrderByDescending(r => r.Id);
                    break;
            }
            return result.ThenByDescending(r => r.Id);
        }

    }
}
