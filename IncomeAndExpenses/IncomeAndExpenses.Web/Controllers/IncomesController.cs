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
    public class IncomesController : BaseController
    {
        public IncomesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Incomes/Create
        public ActionResult Create()
        {
            return View(CreateIncomeCUViewModel(null));
        }

        // POST: Incomes/Create
        [HttpPost]
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
                catch
                {
                    return View(CreateIncomeCUViewModel(income));
                }
            }
            else
            {
                return View(CreateIncomeCUViewModel(income));
            }
        }

        // GET: Incomes/Edit/1
        public ActionResult Edit(int id)
        {
            return View(CreateIncomeCUViewModel(id));
        }

        // POST: Incomes/Edit/1
        [HttpPost]
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
                catch
                {
                    return View(CreateIncomeCUViewModel(income));
                }
            }
            else
            {
                return View(CreateIncomeCUViewModel(income));
            }

        }

        // GET: Incomes/Details/1
        public ActionResult Details(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<Income>().Get(id)));
        }

        // GET: Incomes/Delete/1
        public ActionResult Delete(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<Income>().Get(id)));
        }

        // POST: Incomes/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, IncomeViewModel incomeVM)
        {
            try
            {
                _unitOfWork.Repository<Income>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(ViewModelFromModel(_unitOfWork.Repository<Income>().Get(id)));
            }
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(int id)
        {
            Income income = _unitOfWork.Repository<Income>().Get(id);
            return new IncomeCUViewModel { Income = ViewModelFromModel(income), IncomeTypes = CreateTypesList(income) };
        }

        private IncomeCUViewModel CreateIncomeCUViewModel(Income income)
        {            
            return new IncomeCUViewModel { Income = ViewModelFromModel(income), IncomeTypes = CreateTypesList(income) };
        }

        private IEnumerable<SelectListItem> CreateTypesList(Income income)
        {
            return _unitOfWork.Repository<IncomeType>().GetAll()
               .Where(t => t.UserId == UserId)
               .OrderBy(t => t.Name)
               .ToList()
               .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name, Selected = (income == null ? false : income.IncomeTypeId == t.Id) });
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