using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //Count of incomes or expenses per page
        private const int PAGE_SIZE = 10;

        /// <summary>
        /// Creates controller with UnitOfWork instance to connect with database
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork implementation to connect with database</param>
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET request Home
        /// </summary>
        /// <param name="model">ViewModel for GET request</param>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var incomeTotal = _unitOfWork.Repository<IncomeType>().All()
                .Where(t => t.UserId == UserId)
                .Sum(t => t.Incomes.Sum(i => i.Amount));
            var expenseTotal = _unitOfWork.Repository<ExpenseType>().All()
                .Where(t => t.UserId == UserId)
                .Sum(t => t.Expenses.Sum(e => e.Amount));
            var currentBalance = incomeTotal - expenseTotal;
            var homeViewModel = new HomeIndexViewModel { ExpenseTotal = expenseTotal, IncomeTotal = incomeTotal, CurrentBalance = currentBalance };
            return View(homeViewModel);
        }

        /// <summary>
        /// GET request GetExpensesData
        /// </summary>
        /// <param name="pageNumber">Current page</param>
        /// <param name="searchValue">Value for search</param>
        /// <param name="sortCol">Name of the sorting column</param>
        /// <param name="sortDir">Sorting direction</param>
        /// <returns>Partial view with list of expenses</returns>
        [HttpGet]
        public PartialViewResult GetExpensesData(int pageNumber = 1, string searchValue = "", string sortCol = nameof(ExpenseViewModel.Date), SortDirection sortDir = SortDirection.Descending)
        {
            var expenses = _unitOfWork.Repository<ExpenseType>().All()
                .Where(t => t.UserId == UserId)
                .Join(_unitOfWork.Repository<Expense>().All(), t => t.Id, e => e.ExpenseTypeId,
                    (t, e) => new ExpenseViewModel { Id = e.Id, Amount = e.Amount, Date = e.Date, ExpenseTypeName = t.Name });
            decimal searchValueDec;
            if (decimal.TryParse(searchValue, out searchValueDec))
            {
                searchValue = searchValue.Replace(',', '.');
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                expenses = expenses.Where(e => e.Amount.ToString().Contains(searchValue) || e.ExpenseTypeName.Contains(searchValue));
            }
            var expensesPageInfo = new PageInfoViewModel { PageNumber = pageNumber, PageSize = PAGE_SIZE, TotalItems = expenses.Count() };
            expenses = SortExpenseViewModel(expenses, sortCol, sortDir).Skip((pageNumber - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            var expenseSortInfo = new SortInfoViewModel { ColumnName = sortCol, Direction = sortDir };
            var homeExpenseViewModel = new HomeExpenseViewModel
            {
                Expenses = expenses,
                PageInfo = expensesPageInfo,
                SortInfo = expenseSortInfo,
                SearchValue = searchValue
            };
            return PartialView("ExpensesList", homeExpenseViewModel);
        }

        /// <summary>
        /// GET request GetIncomesData
        /// </summary>
        /// <param name="pageNumber">Current page</param>
        /// <param name="searchValue">Value for search</param>
        /// <param name="sortCol">Name of the sorting column</param>
        /// <param name="sortDir">Sorting direction</param>
        /// <returns>Partial view with list of incomes</returns>
        [HttpGet]
        public PartialViewResult GetIncomesData(int pageNumber = 1, string searchValue = "", string sortCol = nameof(IncomeViewModel.Date), SortDirection sortDir = SortDirection.Descending)
        {
            var incomes = _unitOfWork.Repository<IncomeType>().All()
                .Where(t => t.UserId == UserId)
                .Join(_unitOfWork.Repository<Income>().All(), t => t.Id, i => i.IncomeTypeId,
                    (t, i) => new IncomeViewModel { Id = i.Id, Amount = i.Amount, Date = i.Date, IncomeTypeName = t.Name });
            decimal searchValueDec;
            if (decimal.TryParse(searchValue, out searchValueDec))
            {
                searchValue = searchValue.Replace(',', '.');
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                incomes = incomes.Where(i => i.Amount.ToString().Contains(searchValue) || i.IncomeTypeName.Contains(searchValue));
            }
            var incomesPageInfo = new PageInfoViewModel { PageNumber = pageNumber, PageSize = PAGE_SIZE, TotalItems = incomes.Count() };
            incomes = SortIncomeViewModel(incomes, sortCol, sortDir).Skip((pageNumber - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            var incomesSortInfo = new SortInfoViewModel { ColumnName = sortCol, Direction = sortDir };
            var homeIncomeViewModel = new HomeIncomeViewModel
            {
                Incomes = incomes,
                PageInfo = incomesPageInfo,
                SortInfo = incomesSortInfo,
                SearchValue = searchValue
            };
            return PartialView("IncomesList", homeIncomeViewModel);
        }

        private IQueryable<ExpenseViewModel> SortExpenseViewModel(IQueryable<ExpenseViewModel> expenses, string colName, SortDirection sortDir)
        {
            var result = expenses;
            switch (colName)
            {
                case nameof(ExpenseViewModel.Amount):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(ExpenseViewModel.Date):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(ExpenseViewModel.ExpenseTypeName):
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

        private IQueryable<IncomeViewModel> SortIncomeViewModel(IQueryable<IncomeViewModel> expenses, string colName, SortDirection sortDir)
        {
            var result = expenses;
            switch (colName)
            {
                case nameof(IncomeViewModel.Amount):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Amount).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(IncomeViewModel.Date):
                    if (sortDir == SortDirection.Ascending)
                    {
                        result = result.OrderBy(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    else
                    {
                        result = result.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    }
                    break;
                case nameof(IncomeViewModel.IncomeTypeName):
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