using AutoMapper;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.BusinessLogic;
using IncomeAndExpenses.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{

    [Authorize]
    public class ExpensesController : BaseController
    {
        /// <summary>
        /// Creates controller with IBusinessLogic instance
        /// </summary>
        /// <param name="businessLogic">IBusinessLogic implementation to work with data</param>
        public ExpensesController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        // GET: Expenses/Create
        [HttpGet]
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
                    _businessLogic.CreateExpense(expense);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    ViewData["Error"] = ErrorMessage;
                    return View(CreateExpenseCUViewModel(expense));
                }
            }
            else
            {
                return View(CreateExpenseCUViewModel(expense));
            }
        }

        // GET: Expenses/Edit/1
        [HttpGet]
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
                    _businessLogic.UpdateExpense(expense);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    ViewData["Error"] = ErrorMessage;
                    return View(CreateExpenseCUViewModel(expense));
                }
            }
            else
            {
                return View(CreateExpenseCUViewModel(expense));
            }
        }

        // GET: Expenses/Details/1
        [HttpGet]
        public ActionResult Details(int id)
        {

            return View(ViewModelFromModel(_businessLogic.GetExpense(id)));
        }

        // GET: Expenses/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_businessLogic.GetExpense(id)));
        }

        // POST: Expenses/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _businessLogic.DeleteExpense(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewData["Error"] = ErrorMessage;
                return View(ViewModelFromModel(_businessLogic.GetExpense(id)));
            }
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(int id)
        {
            Expense expense = _businessLogic.GetExpense(id);
            return CreateExpenseCUViewModel(expense);
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(Expense expense)
        {
            return new ExpenseCUViewModel { Expense = ViewModelFromModel(expense), ExpenseTypes = CreateTypesList(expense) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Expense expense)
        {
            return _businessLogic.GetAllExpenseTypes(UserId)
               .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name, Selected = expense?.ExpenseTypeId == t.Id });
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