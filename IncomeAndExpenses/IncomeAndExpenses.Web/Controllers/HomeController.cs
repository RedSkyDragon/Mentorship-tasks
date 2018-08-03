using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using AutoMapper;
using System.Collections.Generic;
using IncomeAndExpenses.BusinessLogic;

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
        public HomeController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        /// <summary>
        /// GET request Home
        /// </summary>
        /// <param name="model">ViewModel for GET request</param>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var totals = _businessLogic.GetTotals(UserId);
            var homeViewModel = new MapperConfiguration(cfg => cfg.CreateMap<HomeIndexViewModel, TotalsBLModel>()).CreateMapper().Map<TotalsBLModel, HomeIndexViewModel>(totals);
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
            decimal searchValueDec;
            if (decimal.TryParse(searchValue, out searchValueDec))
            {
                searchValue = searchValue.Replace(',', '.');
            }
            var expensesBL = _businessLogic.GetAllExpenses(UserId, PAGE_SIZE, pageNumber, searchValue, sortCol, sortDir);
            var expensesPageInfo = new PageInfoViewModel { PageNumber = pageNumber, PageSize = PAGE_SIZE, TotalItems = expensesBL.Count };
            var expenseSortInfo = new SortInfoViewModel { ColumnName = sortCol, Direction = sortDir };
            var homeExpenseViewModel = new HomeExpenseViewModel
            {
                Expenses = ViewModelsFromBLModels(expensesBL.Expenses),
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
            decimal searchValueDec;
            if (decimal.TryParse(searchValue, out searchValueDec))
            {
                searchValue = searchValue.Replace(',', '.');
            }
            var incomesBL = _businessLogic.GetAllIncomes(UserId, PAGE_SIZE, pageNumber, searchValue, sortCol, sortDir);
            var incomesPageInfo = new PageInfoViewModel { PageNumber = pageNumber, PageSize = PAGE_SIZE, TotalItems = incomesBL.Count };
            var incomesSortInfo = new SortInfoViewModel { ColumnName = sortCol, Direction = sortDir };
            var homeIncomeViewModel = new HomeIncomeViewModel
            {
                Incomes = ViewModelsFromBLModels(incomesBL.Incomes),
                PageInfo = incomesPageInfo,
                SortInfo = incomesSortInfo,
                SearchValue = searchValue
            };
            return PartialView("IncomesList", homeIncomeViewModel);
        }

        private IEnumerable<IncomeViewModel> ViewModelsFromBLModels(IEnumerable<IncomeBLModel> incomes)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IncomeViewModel, IncomeBLModel>()).CreateMapper();
            var result = new List<IncomeViewModel>();
            foreach (var income in incomes)
            {
                result.Add(mapper.Map<IncomeBLModel, IncomeViewModel>(income));
            }
            return result;
        }

        private IEnumerable<ExpenseViewModel> ViewModelsFromBLModels(IEnumerable<ExpenseBLModel> expenses)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseViewModel, ExpenseBLModel>()).CreateMapper();
            var result = new List<ExpenseViewModel>();
            foreach (var expense in expenses)
            {
                result.Add(mapper.Map<ExpenseBLModel, ExpenseViewModel>(expense));
            }
            return result;
        }
    }

}