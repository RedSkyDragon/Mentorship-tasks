using AutoMapper;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class IncomeTypesController : BaseController
    {
        /// <summary>
        /// Creates controller with UnitOfWork instance to connect with database
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork implementation to connect with database</param>
        public IncomeTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: IncomeTypes
        public ActionResult Index()
        {
            return View(_unitOfWork.Repository<IncomeType>().All().Where(t=>t.UserId == UserId).ToList().Select(t => ViewModelFromModel(t)).OrderBy(t => t.Name));
        }

        // GET: IncomeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncomeTypes/Create
        [HttpPost]
        public ActionResult Create(IncomeTypeViewModel typeVM)
        {
            IncomeType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<IncomeType>().Create(type);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(typeVM);
                }
            }
            else
            {
                return View(typeVM);
            }
        }

        // GET: IncomeTypes/Edit/1
        public ActionResult Edit(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<IncomeType>().Get(id)));
        }

        // POST: IncomeTypes/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, IncomeTypeViewModel typeVM)
        {
            IncomeType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<IncomeType>().Update(type);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(typeVM);
                }
            }
            else
            {
                return View(typeVM);
            }
        }

        // GET: IncomeTypes/Delete/1
        public ActionResult Delete(int id)
        {
            return View(CreateDeleteViewModel(id));
        }

        // POST: IncomeTypes//Delete/1
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var str = collection["DeleteAll"];
                bool delAll = bool.Parse(collection["DeleteAll"].Split(',')[0]);
                var incomes = _unitOfWork.Repository<Income>().All().Where(ex => ex.IncomeTypeId == id);
                if (delAll)
                {
                    foreach (var income in incomes)
                    {
                        _unitOfWork.Repository<Income>().Delete(income.Id);
                    }
                    _unitOfWork.Repository<IncomeType>().Delete(id);
                }
                else
                {
                    int newTypeId = int.Parse(collection["ReplacementTypeId"]);
                    foreach (var income in incomes)
                    {
                        var upd = new Income { Id = income.Id, Amount = income.Amount, Comment = income.Comment, Date = income.Date, IncomeTypeId = newTypeId };
                        _unitOfWork.Repository<Income>().Update(upd);
                    }
                    _unitOfWork.Repository<IncomeType>().Delete(id);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(CreateDeleteViewModel(id));
            }
        }

        private DeleteIncomeTypeViewModel CreateDeleteViewModel(int id)
        {
            var type = _unitOfWork.Repository<IncomeType>().Get(id);
            var replace = _unitOfWork.Repository<IncomeType>().All()
                .Where(t => t.UserId == type.UserId && t.Id != type.Id)
                .OrderBy(t => t.Name)
                .ToList()
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