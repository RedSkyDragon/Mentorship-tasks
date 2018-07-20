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
        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Income, IncomeViewModel>();

            });           
            var user = _unitOfWork.Repository<string, User>().Get(UserId);
            var iTypes = _unitOfWork.Repository<int, IncomeType>().GetAll().Where(it => it.UserId == user.Id);
            var eTypes = _unitOfWork.Repository<int, ExpenseType>().GetAll().Where(it => it.UserId == user.Id);
            List<Income> incomes = new List<Income>();
            List<Expense> expenses = new List<Expense>();
            foreach (var type in  iTypes)
            {
                incomes = incomes.Concat(type.Incomes).ToList();
            }
            foreach (var type in eTypes)
            {
                expenses = expenses.Concat(type.Expenses).ToList();
            }
            var incomeTotal = incomes.Sum(i => i.Amount);
            var expenseTotal = expenses.Sum(e => e.Amount);
            var currentBalance = incomeTotal - expenseTotal;
            var incomeView = incomes.OrderByDescending(i => i.Date).Select(i => config.CreateMapper().Map<Income, IncomeViewModel>(i));
            var expenseView = expenses.OrderByDescending(e => e.Date).Select(i => config.CreateMapper().Map<Expense, ExpenseViewModel>(i));
            var homeViewModel = new HomeIndexViewModel { Expenses = expenseView, Incomes = incomeView, ExpenseTotal = expenseTotal, IncomeTotal = incomeTotal, CurrentBalance = currentBalance };
            return View(homeViewModel);
        }

    }

}