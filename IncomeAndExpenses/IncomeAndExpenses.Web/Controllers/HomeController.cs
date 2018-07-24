using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using System.Security.Claims;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET Home
        public ActionResult Index(int expensesPage = 1, int incomesPage = 1)
        {
            var incomes = GetAllIncomeViewModels();
            var expenses = GetAllExpenseViewModels();
            var incomeTotal = incomes.Sum(i => i.Amount);
            var expenseTotal = expenses.Sum(e => e.Amount);
            var currentBalance = incomeTotal - expenseTotal;
            int pageSize = 3;
            var incomesPerPages = incomes.Skip((incomesPage - 1) * pageSize).Take(pageSize);
            var expensesPerPages = expenses.Skip((expensesPage - 1) * pageSize).Take(pageSize);
            PageInfoViewModel incomesPageInfo = new PageInfoViewModel { PageNumber = incomesPage, PageSize = pageSize, TotalItems = incomes.Count() };
            PageInfoViewModel expensesPageInfo = new PageInfoViewModel { PageNumber = expensesPage, PageSize = pageSize, TotalItems = expenses.Count() };
            var homeViewModel = new HomeIndexViewModel
            {
                Expenses = expensesPerPages, Incomes = incomesPerPages, ExpenseTotal = expenseTotal, IncomeTotal = incomeTotal, CurrentBalance = currentBalance, ExpensesPageInfo = expensesPageInfo, IncomesPageInfo = incomesPageInfo
            };
            return View(homeViewModel);
        }

        private IEnumerable<IncomeViewModel> GetAllIncomeViewModels()
        {
            var iTypes = _unitOfWork.Repository<int, IncomeType>().GetAll().Where(it => it.UserId == UserId);
            IEnumerable<Income> incomes = null;
            foreach (var type in iTypes)
            {
                incomes = incomes?.Concat(type.Incomes) ?? type.Incomes;
            }
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Income, IncomeViewModel>();

            });
            return incomes.Select(i => config.CreateMapper().Map<IncomeViewModel>(i));
        }

        private IEnumerable<ExpenseViewModel> GetAllExpenseViewModels()
        {
            var eTypes = _unitOfWork.Repository<int, ExpenseType>().GetAll().Where(it => it.UserId == UserId);
            IEnumerable<Expense> expenses = null;
            foreach (var type in eTypes)
            {
                expenses = expenses?.Concat(type.Expenses) ?? type.Expenses;
            }
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Expense, ExpenseViewModel>();

            });
            return expenses.Select(i => config.CreateMapper().Map<ExpenseViewModel>(i));
        }

    }

}