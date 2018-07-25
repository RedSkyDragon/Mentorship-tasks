using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using System.Security.Claims;
using System.Web.Helpers;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private const int PAGE_SIZE = 10;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET Home
        public ActionResult Index(HomeGetViewModel model)
        {
            model = model ?? new HomeGetViewModel();
            var incomes = SortIncomeViewModel(GetAllIncomeViewModels(), model.IncomeSortCol, model.IncomeSortDir);
            var expenses = SortExpenseViewModel(GetAllExpenseViewModels(), model.ExpenseSortCol, model.ExpenseSortDir);
            var incomeTotal = incomes.Sum(i => i.Amount);
            var expenseTotal = expenses.Sum(e => e.Amount);
            var currentBalance = incomeTotal - expenseTotal;
            var incomesPerPages = incomes.Skip((model.IncomePage - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            var expensesPerPages = expenses.Skip((model.ExpensePage - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            var incomesPageInfo = new PageInfoViewModel { PageNumber = model.IncomePage, PageSize = PAGE_SIZE, TotalItems = incomes.Count() };
            var expensesPageInfo = new PageInfoViewModel { PageNumber = model.ExpensePage, PageSize = PAGE_SIZE, TotalItems = expenses.Count() };
            var expenseSortInfo = new SortInfoViewModel { ColumnName = model.ExpenseSortCol, Direction = model.ExpenseSortDir };
            var incomeSortInfo = new SortInfoViewModel { ColumnName = model.IncomeSortCol, Direction = model.IncomeSortDir };
            var homeViewModel = new HomeIndexViewModel
            {
                Expenses = expensesPerPages,
                Incomes = incomesPerPages,
                ExpenseTotal = expenseTotal,
                IncomeTotal = incomeTotal,
                CurrentBalance = currentBalance,
                ExpensesPageInfo = expensesPageInfo,
                IncomesPageInfo = incomesPageInfo,
                ExpensesSortInfo = expenseSortInfo,
                IncomesSortInfo = incomeSortInfo
            };
            return View(homeViewModel);
        }

        private IQueryable<IncomeViewModel> GetAllIncomeViewModels()
        {
            var iTypes = _unitOfWork.Repository<IncomeType>().GetAll().Where(it => it.UserId == UserId);
            IQueryable<Income> incomes = null;
            foreach (var type in iTypes)
            {
                incomes = incomes?.Concat(type.Incomes) ?? type.Incomes.AsQueryable();
            }
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Income, IncomeViewModel>();

            });
            return incomes.Select(i => config.CreateMapper().Map<IncomeViewModel>(i));
        }

        private IQueryable<ExpenseViewModel> GetAllExpenseViewModels()
        {
            var eTypes = _unitOfWork.Repository<ExpenseType>().GetAll().Where(it => it.UserId == UserId);
            IQueryable<Expense> expenses = null;
            foreach (var type in eTypes)
            {
                expenses = expenses?.Concat(type.Expenses) ?? type.Expenses.AsQueryable();
            }
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Expense, ExpenseViewModel>();

            });
            return expenses.Select(i => config.CreateMapper().Map<ExpenseViewModel>(i));
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