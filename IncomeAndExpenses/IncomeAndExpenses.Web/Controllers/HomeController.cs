using IncomeAndExpenses.Web.Models;
using System.Web.Mvc;
using AutoMapper;
using System.Collections.Generic;
using IncomeAndExpenses.BusinessLogic;
using IncomeAndExpenses.BusinessLogic.Models;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private ITotalsBL _totalsBL;
        private IExpensesBL _expensesBL;
        private IIncomesBL _incomesBL;

        /// <summary>
        /// Creates controller with ITotalsBL, IExpensesBL, IIncomesBL instances to connect with database
        /// </summary>
        /// <param name="totalsBL">ITotalsBL implementation to connect with database</param>
        /// /// <param name="expensesBL">IExpensesBL implementation to connect with database</param>
        /// /// <param name="incomesBL">IIncomesBL implementation to connect with database</param>
        public HomeController(ITotalsBL totalsBL, IExpensesBL expensesBL, IIncomesBL incomesBL)
        {
            _totalsBL = totalsBL;
            _expensesBL = expensesBL;
            _incomesBL = incomesBL;
        }

        /// <summary>
        /// GET request Home
        /// </summary>
        /// <param name="model">ViewModel for GET request</param>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var totals = _totalsBL.GetTotals(UserId);
            var homeViewModel = new MapperConfiguration(cfg => cfg.CreateMap<HomeIndexViewModel, Totals>())
                .CreateMapper().Map<Totals, HomeIndexViewModel>(totals);
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
            FilterBL blFilter = CreateBLFilter(filter);
            var expensesBL = _expensesBL.GetAllExpenses(blFilter);
            var expensesPageInfo = new PageInfoViewModel
            {
                PageNumber = blFilter.PageNumber,
                PageSize = blFilter.PageSize,
                TotalItems = expensesBL.TotalCount
            };
            var expenseSortInfo = new SortInfoViewModel
            {
                ColumnName = blFilter.SortCol,
                Direction = blFilter.SortDir
            };
            var homeExpenseViewModel = new HomeExpenseViewModel
            {
                Expenses = ViewModelsFromBLModels(expensesBL.ExpensesList),
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
            FilterBL blFilter = CreateBLFilter(filter);
            var incomesBL = _incomesBL.GetAllIncomes(blFilter);
            var incomesPageInfo = new PageInfoViewModel
            {
                PageNumber = blFilter.PageNumber,
                PageSize = blFilter.PageSize,
                TotalItems = incomesBL.TotalCount
            };
            var incomesSortInfo = new SortInfoViewModel
            {
                ColumnName = blFilter.SortCol,
                Direction = blFilter.SortDir
            };
            var homeIncomeViewModel = new HomeIncomeViewModel
            {
                Incomes = ViewModelsFromBLModels(incomesBL.IncomesList),
                PageInfo = incomesPageInfo,
                SortInfo = incomesSortInfo,
                Filter = filter
            };
            return PartialView("IncomesList", homeIncomeViewModel);
        }

        private FilterBL CreateBLFilter(FilterViewModel filter)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<FilterViewModel, FilterBL>())
                .CreateMapper();
            var result = mapper.Map<FilterViewModel, FilterBL>(filter);
            result.UserId = UserId;
            return result;
        }

        private IEnumerable<IncomeViewModel> ViewModelsFromBLModels(IEnumerable<Income> incomes)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Income, IncomeViewModel>()
                .ForMember(dest => dest.IncomeTypeId, opts => opts.MapFrom(source => source.TypeId))
                .ForMember(dest => dest.IncomeTypeName, opts => opts.MapFrom(source => source.TypeName)))
                .CreateMapper() ;
            var result = new List<IncomeViewModel>();
            foreach (var income in incomes)
            {
                result.Add(mapper.Map<Income, IncomeViewModel>(income));
            }
            return result;
        }

        private IEnumerable<ExpenseViewModel> ViewModelsFromBLModels(IEnumerable<Expense> expenses)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Expense, ExpenseViewModel > ()
                .ForMember(dest => dest.ExpenseTypeId, opts => opts.MapFrom(source => source.TypeId))
                .ForMember(dest => dest.ExpenseTypeName, opts => opts.MapFrom(source => source.TypeName)))
                .CreateMapper();
            var result = new List<ExpenseViewModel>();
            foreach (var expense in expenses)
            {
                result.Add(mapper.Map<Expense, ExpenseViewModel>(expense));
            }
            return result;
        }
    }

}