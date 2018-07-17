using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{

    [Authorize]
    public class ExpensesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ExpensesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(CreateExpenseViewModel(null));
        }

        // POST: Expenses/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var expense = new Expense { Amount = decimal.Parse(collection["Expense.Amount"]), Date = DateTime.Parse(collection["Expense.Date"]), Comment = collection["Expense.Comment"], ExpenseTypeId = int.Parse(collection["Expense.ExpenseTypeId"]) };
            try
            {
                _unitOfWork.Repository<int, Expense>().Create(expense);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(CreateExpenseViewModel(expense));
            }
        }

        // GET: Expenses/Edit/1
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(CreateExpenseViewModel(id));
        }

        // POST: Expenses/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var expense = new Expense { Id = id, Amount = decimal.Parse(collection["Expense.Amount"]), Date = DateTime.Parse(collection["Expense.Date"]), Comment = collection["Expense.Comment"], ExpenseTypeId = int.Parse(collection["Expense.ExpenseTypeId"]) };
            try
            {
                _unitOfWork.Repository<int, Expense>().Update(expense);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(CreateExpenseViewModel(expense));
            }
        }

        // GET: Expenses/Details/1
        public ActionResult Details(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, Expense>().Get(id));
        }

        // GET: Expenses/Delete/1
        public ActionResult Delete(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, Expense>().Get(id));
        }

        // POST: Expenses/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.Repository<int, Expense>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(_unitOfWork.Repository<int, Expense>().Get(id));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private ExpenseViewModel CreateExpenseViewModel(int id)
        {
            Expense expense = _unitOfWork.Repository<int, Expense>().Get(id);
            return new ExpenseViewModel { Expense = expense, ExpenseTypes = CreateTypesList(expense) };
        }

        private ExpenseViewModel CreateExpenseViewModel(Expense expense)
        {           
            return new ExpenseViewModel { Expense = expense, ExpenseTypes = CreateTypesList(expense) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Expense expense)
        {
            return _unitOfWork.Repository<int, ExpenseType>().GetAll()
               .Where(it => it.UserId == User.Identity.Name)
               .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name, Selected = expense?.ExpenseTypeId == t.Id });
        }
    }
}