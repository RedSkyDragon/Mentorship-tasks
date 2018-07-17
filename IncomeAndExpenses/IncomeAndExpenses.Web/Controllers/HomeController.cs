using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            var user = _unitOfWork.Repository<string, User>().Get(User.Identity.Name);
            ViewBag.UserName = user.UserName;
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
            var homeViewModel = new HomeIndexViewModel { User = user, Expenses = expenses, Incomes = incomes };
            return View(homeViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

}