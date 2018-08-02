﻿using AutoMapper;
using IncomeAndExpenses.DataAccessInterface;
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
        /// Creates controller with UnitOfWork instance to connect with database
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork implementation to connect with database</param>
        public IncomesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    _unitOfWork.Repository<Income>().Create(income);
                    _unitOfWork.Save();
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
                    _unitOfWork.Repository<Income>().Update(income);
                    _unitOfWork.Save();
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
            return View(ViewModelFromModel(_unitOfWork.Repository<Income>().Get(id)));
        }

        // GET: Incomes/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<Income>().Get(id)));
        }

        // POST: Incomes/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IncomeViewModel incomeVM)
        {
            try
            {
                _unitOfWork.Repository<Income>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewData["Error"] = ErrorMessage;
                return View(ViewModelFromModel(_unitOfWork.Repository<Income>().Get(id)));
            }
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(int id)
        {
            Income income = _unitOfWork.Repository<Income>().Get(id);
            return CreateIncomeCUViewModel(income);
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(Income income)
        {            
            return new IncomeCUViewModel { Income = ViewModelFromModel(income), IncomeTypes = CreateTypesList(income) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Income income)
        {
            return _unitOfWork.Repository<IncomeType>().All()
               .Where(t => t.UserId == UserId)
               .OrderBy(t => t.Name)
               .ToList()
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