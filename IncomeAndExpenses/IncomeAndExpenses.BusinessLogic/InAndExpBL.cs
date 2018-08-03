using System.Collections.Generic;
using System.Linq;
using IncomeAndExpenses.DataAccessInterface;
using System.Web.Helpers;

namespace IncomeAndExpenses.BusinessLogic
{
    public class InAndExpBL : IBusinessLogic
    {
        private IUnitOfWork _unitOfWork;

        public InAndExpBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ///<inheritdoc/>
        public void CreateExpense(Expense expense)
        {
            _unitOfWork.Repository<Expense>().Create(expense);
            _unitOfWork.Save();
        }

        public void CreateExpenseType(ExpenseType type)
        {
            _unitOfWork.Repository<ExpenseType>().Create(type);
            _unitOfWork.Save();
        }

        public void CreateIncome(Income income)
        {
            _unitOfWork.Repository<Income>().Create(income);
            _unitOfWork.Save();
        }

        public void CreateIncomeType(IncomeType type)
        {
            _unitOfWork.Repository<IncomeType>().Create(type);
            _unitOfWork.Save();
        }

        public void DeleteExpense(int id)
        {
            _unitOfWork.Repository<Expense>().Delete(id);
            _unitOfWork.Save();
        }

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

        public void DeleteIncome(int id)
        {
            _unitOfWork.Repository<Income>().Delete(id);
            _unitOfWork.Save();
        }

        public void Dispose() { }

        public User GetUser(string id)
        {
            return _unitOfWork.Repository<string, User>().Get(id);
        }

        public void CreateUser(User user)
        {
            _unitOfWork.Repository<string, User>().Create(user);
            _unitOfWork.Repository<IncomeType>().Create(new IncomeType { UserId = user.Id, Name = "Other", Description = "Income that are difficult to classify as specific type." });
            _unitOfWork.Repository<ExpenseType>().Create(new ExpenseType { UserId = user.Id, Name = "Other", Description = "Expense that are difficult to classify as specific type." });
            _unitOfWork.Save();
        }

        public void UpdateUser(User user)
        {
            _unitOfWork.Repository<string, User>().Update(user);
            _unitOfWork.Save();
        }

        public Expense GetExpense(int id)
        {
            return _unitOfWork.Repository<Expense>().Get(id);
        }

        public ExpenseType GetExpenseType(int id)
        {
            return _unitOfWork.Repository<ExpenseType>().Get(id);
        }

        public Income GetIncome(int id)
        {
            return _unitOfWork.Repository<Income>().Get(id);
        }

        public IncomeType GetIncomeType(int id)
        {
            return _unitOfWork.Repository<IncomeType>().Get(id);
        }

        public ExpensesBLModel GetAllExpenses(string userId, int pageSize = 10, int pageNumber = 1, string searchValue = "", string sortCol = nameof(IncomeBLModel.Date), SortDirection sortDir = SortDirection.Descending)
        {
            var expenses = _unitOfWork.Repository<ExpenseType>().All()
                 .Where(t => t.UserId == userId)
                 .Join(_unitOfWork.Repository<Expense>().All(), t => t.Id, e => e.ExpenseTypeId,
                     (t, e) => new ExpenseBLModel { Id = e.Id, Amount = e.Amount, Date = e.Date, ExpenseTypeName = t.Name });
            if (!string.IsNullOrEmpty(searchValue))
            {
                expenses = expenses.Where(e => e.Amount.ToString().Contains(searchValue) || e.ExpenseTypeName.Contains(searchValue));
            }
            int count = expenses.Count();
            expenses = SortExpenseBLModel(expenses, sortCol, sortDir).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new ExpensesBLModel { Expenses = expenses, Count = count };
        }

        public IEnumerable<ExpenseType> GetAllExpenseTypes(string userId)
        {
            return _unitOfWork.Repository<ExpenseType>().All().Where(t => t.UserId == userId).OrderBy(t => t.Name);
        }

        public IncomesBLModel GetAllIncomes(string userId, int pageSize = 10, int pageNumber = 1, string searchValue = "", string sortCol = nameof(IncomeBLModel.Date), SortDirection sortDir = SortDirection.Descending)
        {
            var incomes = _unitOfWork.Repository<IncomeType>().All()
                .Where(t => t.UserId == userId)
                .Join(_unitOfWork.Repository<Income>().All(), t => t.Id, i => i.IncomeTypeId,
                    (t, i) => new IncomeBLModel { Id = i.Id, Amount = i.Amount, Date = i.Date, IncomeTypeName = t.Name });
            if (!string.IsNullOrEmpty(searchValue))
            {
                incomes = incomes.Where(i => i.Amount.ToString().Contains(searchValue) || i.IncomeTypeName.Contains(searchValue));
            }
            int count = incomes.Count();
            incomes = SortIncomeViewModel(incomes, sortCol, sortDir).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new IncomesBLModel { Incomes = incomes, Count = count };
        }

        public IEnumerable<IncomeType> GetAllIncomeTypes(string userId)
        {
            return _unitOfWork.Repository<IncomeType>().All().Where(t => t.UserId == userId).OrderBy(t => t.Name);
        }

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

        public void UpdateExpense(Expense expense)
        {
            _unitOfWork.Repository<Expense>().Update(expense);
            _unitOfWork.Save();
        }

        public void UpdateExpenseType(ExpenseType type)
        {
            _unitOfWork.Repository<ExpenseType>().Update(type);
            _unitOfWork.Save();
        }

        public void UpdateIncome(Income income)
        {
            _unitOfWork.Repository<Income>().Update(income);
            _unitOfWork.Save();
        }

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

        private IQueryable<IncomeBLModel> SortIncomeViewModel(IQueryable<IncomeBLModel> expenses, string colName, SortDirection sortDir)
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