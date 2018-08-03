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
    public class IncomeTypesController : BaseController
    {
        /// <summary>
        /// Creates controller with IBusinessLogic instance
        /// </summary>
        /// <param name="businessLogic">IBusinessLogic implementation to work with data</param>
        public IncomeTypesController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        // GET: IncomeTypes
        [HttpGet]
        public ActionResult Index()
        {
            return View(_businessLogic.GetAllIncomeTypes(UserId).Select(t => ViewModelFromModel(t)));
        }

        // GET: IncomeTypes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncomeTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IncomeTypeViewModel typeVM)
        {
            IncomeType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _businessLogic.CreateIncomeType(type);
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

        // GET: IncomeTypes/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(ViewModelFromModel(_businessLogic.GetIncomeType(id)));
        }

        // POST: IncomeTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IncomeTypeViewModel typeVM)
        {
            IncomeType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _businessLogic.UpdateIncomeType(type);
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

        // GET: IncomeTypes/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(CreateDeleteViewModel(id));
        }

        // POST: IncomeTypes//Delete/1
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
                    _businessLogic.DeleteIncomeType(id);
                }
                else
                {
                    int newTypeId = int.Parse(collection["ReplacementTypeId"]);
                    _businessLogic.DeleteAndReplaceIncomeType(id, newTypeId);
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

        private DeleteIncomeTypeViewModel CreateDeleteViewModel(int id)
        {
            var type = _businessLogic.GetIncomeType(id);
            var replace = _businessLogic.GetAllIncomeTypes(UserId)
                .Where(t => t.Id != type.Id)
                .Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });          
            return new DeleteIncomeTypeViewModel { IncomeType = ViewModelFromModel(type), ReplacementTypes = replace };
        }

        private IncomeType ModelFromViewModel(IncomeTypeViewModel typeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncomeTypeViewModel, IncomeType>());
            return config.CreateMapper().Map<IncomeTypeViewModel, IncomeType>(typeVM);
        }

        private IncomeTypeViewModel ViewModelFromModel(IncomeType type)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncomeType, IncomeTypeViewModel>());
            return config.CreateMapper().Map<IncomeType, IncomeTypeViewModel>(type);
        }
    }
}