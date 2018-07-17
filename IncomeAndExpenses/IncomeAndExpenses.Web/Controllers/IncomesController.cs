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
    public class IncomesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public IncomesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Incomes/Create
        public ActionResult Create()
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(CreateIncomeViewModel(null));
        }

        // POST: Incomes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var income = new Income { Amount = decimal.Parse(collection["Income.Amount"]), Date = DateTime.Parse(collection["Income.Date"]), Comment = collection["Income.Comment"], IncomeTypeId = int.Parse(collection["Income.IncomeTypeId"]) };
            try
            {
                _unitOfWork.Repository<int, Income>().Create(income);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(CreateIncomeViewModel(income));
            }
        }

        // GET: Incomes/Edit/1
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(CreateIncomeViewModel(id));
        }

        // POST: Incomes/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var income = new Income { Id = id, Amount = decimal.Parse(collection["Income.Amount"]), Date = DateTime.Parse(collection["Income.Date"]), Comment = collection["Income.Comment"], IncomeTypeId = int.Parse(collection["Income.IncomeTypeId"]) };
            try
            {
                _unitOfWork.Repository<int, Income>().Update(income);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(CreateIncomeViewModel(income));
            }
        }

        // GET: Incomes/Details/1
        public ActionResult Details(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, Income>().Get(id));
        }

        // GET: Incomes/Delete/1
        public ActionResult Delete(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, Income>().Get(id));
        }

        // POST: Incomes/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.Repository<int, Income>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(_unitOfWork.Repository<int, Income>().Get(id));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private IncomeViewModel CreateIncomeViewModel(int id)
        {
            Income income = _unitOfWork.Repository<int, Income>().Get(id);
            return new IncomeViewModel { Income = income, IncomeTypes = CreateTypesList(income) };
        }

        private IncomeViewModel CreateIncomeViewModel(Income income)
        {
            return new IncomeViewModel { Income = income, IncomeTypes = CreateTypesList(income) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Income income)
        {
            return _unitOfWork.Repository<int, IncomeType>().GetAll()
               .Where(it => it.UserId == User.Identity.Name)
               .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name, Selected = income?.IncomeTypeId == t.Id });
        }
    }
}