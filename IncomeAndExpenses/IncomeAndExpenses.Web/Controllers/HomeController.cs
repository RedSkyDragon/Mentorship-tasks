using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using AutoMapper;
using System.Collections.Generic;
using IncomeAndExpenses.BusinessLogic;
using IncomeAndExpenses.BusinessLogic.Models;
using System;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
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
        /// <param name="filter">FilterViewModel for filtration</param>
        /// <returns>Partial view with list of expenses</returns>
        [HttpGet]
        public PartialViewResult GetExpensesData(FilterViewModel filter)
        {
            FilterBLModel blFilter = CreateBLFilter(filter);
            var expensesBL = _businessLogic.GetAllExpenses(blFilter);
            var expensesPageInfo = new PageInfoViewModel { PageNumber = blFilter.PageNumber, PageSize = blFilter.PageSize, TotalItems = expensesBL.Count };
            var expenseSortInfo = new SortInfoViewModel { ColumnName = blFilter.SortCol, Direction = blFilter.SortDir };
            var homeExpenseViewModel = new HomeExpenseViewModel
            {
                Expenses = ViewModelsFromBLModels(expensesBL.Expenses),
                PageInfo = expensesPageInfo,
                SortInfo = expenseSortInfo,
                Filter = filter
            };
            return PartialView("ExpensesList", homeExpenseViewModel);
        }

        /// <summary>
        /// GET request GetIncomesData
        /// </summary>
        /// <param name="filter">FilterViewModel for filtration</param>
        /// <returns>Partial view with list of incomes</returns>
        [HttpGet]
        public PartialViewResult GetIncomesData(FilterViewModel filter)
        {
            FilterBLModel blFilter = CreateBLFilter(filter);
            var incomesBL = _businessLogic.GetAllIncomes(blFilter);
            var incomesPageInfo = new PageInfoViewModel { PageNumber = blFilter.PageNumber, PageSize = blFilter.PageSize, TotalItems = incomesBL.Count };
            var incomesSortInfo = new SortInfoViewModel { ColumnName = blFilter.SortCol, Direction = blFilter.SortDir };
            var homeIncomeViewModel = new HomeIncomeViewModel
            {
                Incomes = ViewModelsFromBLModels(incomesBL.Incomes),
                PageInfo = incomesPageInfo,
                SortInfo = incomesSortInfo,
                Filter = filter
            };
            return PartialView("IncomesList", homeIncomeViewModel);
        }

        private FilterBLModel CreateBLFilter(FilterViewModel filter)
        {
            var result = new FilterBLModel();
            result.MinDate = filter.MinDate.HasValue ? filter.MinDate.Value : result.MinDate;
            result.MaxDate = filter.MaxDate.HasValue ? filter.MaxDate.Value : result.MaxDate;
            result.MinAmount = filter.MinAmount.HasValue ? filter.MinAmount.Value : result.MinAmount;
            result.MaxAmount = filter.MaxAmount.HasValue ? filter.MaxAmount.Value : result.MaxAmount;
            result.UserId = UserId;
            result.TypeName = filter.TypeName;
            result.SortDir = filter.SortDir;
            result.SortCol = filter.SortCol;
            result.PageNumber = filter.PageNumber;
            result.PageSize = filter.PageSize;
            return result;
        }

        private SelectList CreateSizes(int currentSize)
        {
            var sizes = new List<int>();
            sizes.Add(5);
            sizes.Add(10);
            sizes.Add(15);
            sizes.Add(20);
            sizes.Add(30);
            return new SelectList(sizes, currentSize);
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