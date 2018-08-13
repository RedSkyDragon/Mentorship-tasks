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
    public class IncomesController : BaseController
    {
        private IIncomesBL _incomesBL;

        /// <summary>
        /// Creates controller with IIncomesBL instance
        /// </summary>
        /// <param name="incomesBL">IIncomesBL implementation to work with data</param>
        public IncomesController(IIncomesBL incomesBL)
        {
            _incomesBL = incomesBL;
        }

        // GET: Incomes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(CreateIncomeCUViewModel(null));
        }

        // POST: Incomes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IncomeCUViewModel incomeVM)
        {
            IncomeDM income = ModelFromViewModel(incomeVM.Income);
            if (ModelState.IsValid)
            {               
                try
                {
                    _incomesBL.CreateIncome(income);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    ViewData["Error"] = ErrorMessage;
                    return View(CreateIncomeCUViewModel(income));
                }
            }
            else
            {
                return View(CreateIncomeCUViewModel(income));
            }
        }

        // GET: Incomes/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(CreateIncomeCUViewModel(id));
        }

        // POST: Incomes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IncomeCUViewModel incomeVM)
        {
            IncomeDM income = ModelFromViewModel(incomeVM.Income);
            if (ModelState.IsValid)
            {
                try
                {
                    _incomesBL.UpdateIncome(income);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    ViewData["Error"] = ErrorMessage;
                    return View(CreateIncomeCUViewModel(income));
                }
            }
            else
            {
                return View(CreateIncomeCUViewModel(income));
            }

        }

        // GET: Incomes/Details/1
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(ViewModelFromModel(_incomesBL.GetIncome(id)));
        }

        // GET: Incomes/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_incomesBL.GetIncome(id)));
        }

        // POST: Incomes/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IncomeViewModel incomeVM)
        {
            try
            {
                _incomesBL.DeleteIncome(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewData["Error"] = ErrorMessage;
                return View(ViewModelFromModel(_incomesBL.GetIncome(id)));
            }
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(int id)
        {
            IncomeDM income = _incomesBL.GetIncome(id);
            return CreateIncomeCUViewModel(income);
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(IncomeDM income)
        {            
            return new IncomeCUViewModel { Income = ViewModelFromModel(income), IncomeTypes = CreateTypesList(income) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(IncomeDM income)
        {
            return _incomesBL.GetAllIncomeTypes(UserId)
               .Select(t => new SelectListItem {
                   Value = t.Id.ToString(),
                   Text = t.Name,
                   Selected = income?.IncomeTypeId == t.Id
               });
        }

        private IncomeDM ModelFromViewModel(IncomeViewModel incomeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncomeViewModel, IncomeDM>());
            return config.CreateMapper().Map<IncomeViewModel, IncomeDM>(incomeVM);
        }

        private IncomeViewModel ViewModelFromModel(IncomeDM income)
        {
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IncomeDM, IncomeViewModel>()
                .ForMember(destination => destination.IncomeTypeName, opts => opts.MapFrom(source => source.IncomeType.Name)));
            return config.CreateMapper().Map<IncomeDM, IncomeViewModel>(income);
        }
    }
}