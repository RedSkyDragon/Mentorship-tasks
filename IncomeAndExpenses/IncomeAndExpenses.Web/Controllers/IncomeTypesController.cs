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
        private IIncomesBL _incomesBL;

        /// <summary>
        /// Creates controller with IIncomesBL instance
        /// </summary>
        /// <param name="incomesBL">IIncomesBL implementation to work with data</param>
        public IncomeTypesController(IIncomesBL incomesBL)
        {
            _incomesBL = incomesBL;
        }


        // GET: IncomeTypes
        [HttpGet]
        public ActionResult Index()
        {
            return View(_incomesBL.GetAllIncomeTypes(UserId).Select(t => ViewModelFromModel(t)));
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
            IncomeTypeDM type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _incomesBL.CreateIncomeType(type);
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
            return View(ViewModelFromModel(_incomesBL.GetIncomeType(id)));
        }

        // POST: IncomeTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IncomeTypeViewModel typeVM)
        {
            IncomeTypeDM type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _incomesBL.UpdateIncomeType(type);
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
        public ActionResult Delete(int id, DeleteIncomeTypeViewModel model)
        {
            try
            {
                if (model.DeleteAll || !model.ReplacementTypeId.HasValue)
                {
                    _incomesBL.DeleteIncomeType(id);
                }
                else
                {
                    _incomesBL.DeleteAndReplaceIncomeType(id, model.ReplacementTypeId.Value);
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
            var types = _incomesBL.GetAllIncomeTypes(UserId);
            var replace = types.Where(t => t.Id != id)
                .Select(t => new SelectListItem {
                    Text = t.Name,
                    Value = t.Id.ToString()
                });
            var type = types.Where(t => t.Id == id).FirstOrDefault();
            var deleteAll = replace.Count() == 0;
            return new DeleteIncomeTypeViewModel { IncomeType = ViewModelFromModel(type), ReplacementTypes = replace, DeleteAll = deleteAll, ReplacementTypeId = null };
        }

        private IncomeTypeDM ModelFromViewModel(IncomeTypeViewModel typeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncomeTypeViewModel, IncomeTypeDM>());
            return config.CreateMapper().Map<IncomeTypeViewModel, IncomeTypeDM>(typeVM);
        }

        private IncomeTypeViewModel ViewModelFromModel(IncomeTypeDM type)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncomeTypeDM, IncomeTypeViewModel>());
            return config.CreateMapper().Map<IncomeTypeDM, IncomeTypeViewModel>(type);
        }
    }
}