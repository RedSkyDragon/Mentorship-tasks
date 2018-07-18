using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET Home
        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Income, IncomeViewModel>();

            });
            string userId = User.Identity.Name.Split('|').First();
            var user = _unitOfWork.Repository<string, User>().Get(userId);
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
            var incomeView = incomes.Select(i => config.CreateMapper().Map<Income, IncomeViewModel>(i));
            var expenseView = expenses.Select(i => config.CreateMapper().Map<Expense, ExpenseViewModel>(i));
            var homeViewModel = new HomeIndexViewModel { Expenses = expenseView, Incomes = incomeView };
            return View(homeViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

}