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
        private IExpensesBL _expensesBL;

        /// <summary>
        /// Creates controller with IExpensesBL instance
        /// </summary>
        /// <param name="expensesBL">IExpensesBL implementation to work with data</param>
        public ExpensesController(IExpensesBL expensesBL)
        {
            _expensesBL = expensesBL;
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
                    _expensesBL.CreateExpense(expense);
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
                    _expensesBL.UpdateExpense(expense);
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

            return View(ViewModelFromModel(_expensesBL.GetExpense(id)));
        }

        // GET: Expenses/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_expensesBL.GetExpense(id)));
        }

        // POST: Expenses/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _expensesBL.DeleteExpense(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewData["Error"] = ErrorMessage;
                return View(ViewModelFromModel(_expensesBL.GetExpense(id)));
            }
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(int id)
        {
            ExpenseDM expense = _expensesBL.GetExpense(id);
            return CreateExpenseCUViewModel(expense);
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(ExpenseDM expense)
        {
            return new ExpenseCUViewModel { Expense = ViewModelFromModel(expense), ExpenseTypes = CreateTypesList(expense) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(ExpenseDM expense)
        {
            return _expensesBL.GetAllExpenseTypes(UserId)
               .Select(t => new SelectListItem {
                   Value = t.Id.ToString(),
                   Text = t.Name,
                   Selected = expense?.ExpenseTypeId == t.Id
               });
        }

        private ExpenseDM ModelFromViewModel(ExpenseViewModel expenseVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseViewModel, ExpenseDM>());
            return config.CreateMapper().Map<ExpenseViewModel, ExpenseDM>(expenseVM);
        }

        private ExpenseViewModel ViewModelFromModel(ExpenseDM expense)
        {
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ExpenseDM, ExpenseViewModel>()
                .ForMember(
                    destination => destination.ExpenseTypeName, 
                    opts => opts.MapFrom(source => source.ExpenseType.Name)
                    )
            );
            return config.CreateMapper().Map<ExpenseDM, ExpenseViewModel>(expense);
        }
    }
}