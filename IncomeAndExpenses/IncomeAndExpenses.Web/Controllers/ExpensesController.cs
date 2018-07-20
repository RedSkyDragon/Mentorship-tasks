using AutoMapper;
using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{

    [Authorize]
    public class ExpensesController : BaseController
    {
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
        public ActionResult Create(ExpenseCUViewModel expenseVM)
        {
            var expense = ModelFromViewModel(expenseVM.Expense);
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<int, Expense>().Create(expense);
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
        public ActionResult Edit(int id, ExpenseCUViewModel expenseVM)
        {
            var expense = ModelFromViewModel(expenseVM.Expense);
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<int, Expense>().Update(expense);
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

            return View(ViewModelFromModel(_unitOfWork.Repository<int, Expense>().Get(id)));
        }

        // GET: Expenses/Delete/1
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<int, Expense>().Get(id)));
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
                return View(ViewModelFromModel(_unitOfWork.Repository<int, Expense>().Get(id)));
            }
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(int id)
        {
            Expense expense = _unitOfWork.Repository<int, Expense>().Get(id);
            return new ExpenseCUViewModel { Expense = ViewModelFromModel(expense), ExpenseTypes = CreateTypesList(expense) };
        }

        private ExpenseCUViewModel CreateExpenseCUViewModel(Expense expense)
        {
            return new ExpenseCUViewModel { Expense = ViewModelFromModel(expense), ExpenseTypes = CreateTypesList(expense) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Expense expense)
        {
            return _unitOfWork.Repository<int, ExpenseType>().GetAll()
               .Where(t => t.UserId == UserId)
               .OrderBy(t => t.Name)
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