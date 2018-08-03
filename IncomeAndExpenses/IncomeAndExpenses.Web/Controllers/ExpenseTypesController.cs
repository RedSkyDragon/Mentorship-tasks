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
        /// <summary>
        /// Creates controller with IBusinessLogic instance
        /// </summary>
        /// <param name="businessLogic">IBusinessLogic implementation to work with data</param>
        public ExpenseTypesController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        // GET: ExpenseTypes
        [HttpGet]
        public ActionResult Index()
        {
            return View(_businessLogic.GetAllExpenseTypes(UserId).Select(t=> ViewModelFromModel(t)));
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
            ExpenseType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _businessLogic.CreateExpenseType(type);
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
            return View(ViewModelFromModel(_businessLogic.GetExpenseType(id)));
        }

        // POST: ExpenseTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ExpenseTypeViewModel typeVM)
        {
            ExpenseType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _businessLogic.UpdateExpenseType(type);
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var str = collection["DeleteAll"];
                bool delAll = bool.Parse(collection["DeleteAll"].Split(',')[0]);
                if (delAll)
                {
                    _businessLogic.DeleteExpenseType(id);
                }
                else
                {
                    int newTypeId = int.Parse(collection["ReplacementTypeId"]);
                    _businessLogic.DeleteAndReplaceExpenseType(id, newTypeId);
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
            var type = _businessLogic.GetExpenseType(id);
            var replace = _businessLogic.GetAllExpenseTypes(UserId)
                .Where(t => t.Id != type.Id)
                .Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });           
            return new DeleteExpenseTypeViewModel { ExpenseType = ViewModelFromModel(type), ReplacementTypes = replace };
        }

        private ExpenseType ModelFromViewModel(ExpenseTypeViewModel typeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseTypeViewModel, ExpenseType>());
            return config.CreateMapper().Map<ExpenseTypeViewModel, ExpenseType>(typeVM);
        }

        private ExpenseTypeViewModel ViewModelFromModel(ExpenseType type)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>());
            return config.CreateMapper().Map<ExpenseType, ExpenseTypeViewModel>(type);
        }
    }
}