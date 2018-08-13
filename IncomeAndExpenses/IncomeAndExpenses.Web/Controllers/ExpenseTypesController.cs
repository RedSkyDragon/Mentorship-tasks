using AutoMapper;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.BusinessLogic;
using IncomeAndExpenses.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class ExpenseTypesController : BaseController
    {
        private IExpensesBL _expensesBL;

        /// <summary>
        /// Creates controller with IExpensesBL instance
        /// </summary>
        /// <param name="expensesBL">IExpensesBL implementation to work with data</param>
        public ExpenseTypesController(IExpensesBL expensesBL)
        {
            _expensesBL = expensesBL;
        }

        // GET: ExpenseTypes
        [HttpGet]
        public ActionResult Index()
        {
            return View(_expensesBL.GetAllExpenseTypes(UserId).Select(t=> ViewModelFromModel(t)));
        }

        // GET: ExpenseTypes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseTypeViewModel typeVM)
        {
            ExpenseTypeDM type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _expensesBL.CreateExpenseType(type);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    ViewData["Error"] = ErrorMessage;
                    return View(typeVM);
                }
            }
            else
            {
                return View(typeVM);
            }
        }

        // GET: ExpenseTypes/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(ViewModelFromModel(_expensesBL.GetExpenseType(id)));
        }

        // POST: ExpenseTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ExpenseTypeViewModel typeVM)
        {
            ExpenseTypeDM type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _expensesBL.UpdateExpenseType(type);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    ViewData["Error"] = ErrorMessage;
                    return View(typeVM);
                }
            }
            else
            {
                return View(typeVM);
            }
        }

        // GET: ExpenseTypes/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(CreateDeleteViewModel(id));
        }

        // POST: ExpenseTypes/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DeleteExpenseTypeViewModel model)
        {
            try
            {
                if (model.DeleteAll || !model.ReplacementTypeId.HasValue)
                {
                    _expensesBL.DeleteExpenseType(id);
                }
                else
                {
                    _expensesBL.DeleteAndReplaceExpenseType(id, model.ReplacementTypeId.Value);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewData["Error"] = ErrorMessage;
                return View(CreateDeleteViewModel(id));
            }
        }

        private DeleteExpenseTypeViewModel CreateDeleteViewModel(int id)
        {
            var types = _expensesBL.GetAllExpenseTypes(UserId);
            var replace = types.Where(t => t.Id != id)
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                });
            var type = types.Where(t => t.Id == id).FirstOrDefault();
            var deleteAll = replace.Count() == 0;
            return new DeleteExpenseTypeViewModel { ExpenseType = ViewModelFromModel(type), ReplacementTypes = replace, DeleteAll = deleteAll, ReplacementTypeId = null };
        }

        private ExpenseTypeDM ModelFromViewModel(ExpenseTypeViewModel typeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseTypeViewModel, ExpenseTypeDM>());
            return config.CreateMapper().Map<ExpenseTypeViewModel, ExpenseTypeDM>(typeVM);
        }

        private ExpenseTypeViewModel ViewModelFromModel(ExpenseTypeDM type)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseTypeDM, ExpenseTypeViewModel>());
            return config.CreateMapper().Map<ExpenseTypeDM, ExpenseTypeViewModel>(type);
        }
    }
}