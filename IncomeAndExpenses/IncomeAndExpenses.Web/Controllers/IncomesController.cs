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
        /// <summary>
        /// Creates controller with IBusinessLogic instance
        /// </summary>
        /// <param name="businessLogic">IBusinessLogic implementation to work with data</param>
        public IncomesController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
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
            Income income = ModelFromViewModel(incomeVM.Income);
            if (ModelState.IsValid)
            {               
                try
                {
                    _businessLogic.CreateIncome(income);
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
            Income income = ModelFromViewModel(incomeVM.Income);
            if (ModelState.IsValid)
            {
                try
                {
                    _businessLogic.UpdateIncome(income);
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
            return View(ViewModelFromModel(_businessLogic.GetIncome(id)));
        }

        // GET: Incomes/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_businessLogic.GetIncome(id)));
        }

        // POST: Incomes/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IncomeViewModel incomeVM)
        {
            try
            {
                _businessLogic.DeleteIncome(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewData["Error"] = ErrorMessage;
                return View(ViewModelFromModel(_businessLogic.GetIncome(id)));
            }
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(int id)
        {
            Income income = _businessLogic.GetIncome(id);
            return CreateIncomeCUViewModel(income);
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(Income income)
        {            
            return new IncomeCUViewModel { Income = ViewModelFromModel(income), IncomeTypes = CreateTypesList(income) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Income income)
        {
            return _businessLogic.GetAllIncomeTypes(UserId)
               .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name, Selected = income?.IncomeTypeId == t.Id });
        }

        private Income ModelFromViewModel(IncomeViewModel incomeVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IncomeViewModel, Income>());
            return config.CreateMapper().Map<IncomeViewModel, Income>(incomeVM);
        }

        private IncomeViewModel ViewModelFromModel(Income income)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Income, IncomeViewModel>().ForMember(destination => destination.IncomeTypeName, opts => opts.MapFrom(source => source.IncomeType.Name)));
            return config.CreateMapper().Map<Income, IncomeViewModel>(income);
        }
    }
}