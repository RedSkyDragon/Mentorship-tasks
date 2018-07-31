using AutoMapper;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{

    [Authorize]
    public class ExpensesController : BaseController
    {
        /// <summary>
        /// Creates controller with UnitOfWork instance to connect with database
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork implementation to connect with database</param>
        public ExpensesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View(CreateExpenseCUViewModel(null));
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseCUViewModel expenseVM)
        {
            var expense = ModelFromViewModel(expenseVM.Expense);
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<Expense>().Create(expense);
                    _unitOfWork.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View(CreateExpenseCUViewModel(expense));
                }
            }
            else
            {
                return View(CreateExpenseCUViewModel(expense));
            }
        }

        // GET: Expenses/Edit/1
        public ActionResult Edit(int id)
        {
            return View(CreateExpenseCUViewModel(id));
        }

        // POST: Expenses/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ExpenseCUViewModel expenseVM)
        {
            var expense = ModelFromViewModel(expenseVM.Expense);
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<Expense>().Update(expense);
                    _unitOfWork.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View(CreateExpenseCUViewModel(expense));
                }
            }
            else
            {
                return View(CreateExpenseCUViewModel(expense));
            }
        }

        // GET: Expenses/Details/1
        public ActionResult Details(int id)
        {

            return View(ViewModelFromModel(_unitOfWork.Repository<Expense>().Get(id)));
        }

        // GET: Expenses/Delete/1
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<Expense>().Get(id)));
        }

        // POST: Expenses/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.Repository<Expense>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(ViewModelFromModel(_unitOfWork.Repository<Expense>().Get(id)));
            }
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(int id)
        {
            Expense expense = _unitOfWork.Repository<Expense>().Get(id);
            return CreateExpenseCUViewModel(expense);
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(Expense expense)
        {
            return new ExpenseCUViewModel { Expense = ViewModelFromModel(expense), ExpenseTypes = CreateTypesList(expense) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Expense expense)
        {
            return _unitOfWork.Repository<ExpenseType>().All()
               .Where(t => t.UserId == UserId)
               .OrderBy(t => t.Name)
               .ToList()
               .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name, Selected = (expense == null ? false : expense.ExpenseTypeId == t.Id) });
        }

        private Expense ModelFromViewModel(ExpenseViewModel expenseVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseViewModel, Expense>());
            return config.CreateMapper().Map<ExpenseViewModel, Expense>(expenseVM);
        }

        private ExpenseViewModel ViewModelFromModel(Expense expense)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Expense, ExpenseViewModel>().ForMember(destination => destination.ExpenseTypeName, opts => opts.MapFrom(source => source.ExpenseType.Name)));
            return config.CreateMapper().Map<Expense, ExpenseViewModel>(expense);
        }
    }
}