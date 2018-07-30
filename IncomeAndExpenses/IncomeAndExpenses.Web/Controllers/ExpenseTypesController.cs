using AutoMapper;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class ExpenseTypesController : BaseController
    {
        /// <summary>
        /// Creates controller with UnitOfWork instance to connect with database
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork implementation to connect with database</param>
        public ExpenseTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ExpenseTypes
        public ActionResult Index()
        {
            return View(_unitOfWork.Repository<ExpenseType>().All().Where(t => t.UserId == UserId).ToList().Select(t=> ViewModelFromModel(t)).OrderBy(t => t.Name));
        }

        // GET: ExpenseTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        [HttpPost]
        public ActionResult Create(ExpenseTypeViewModel typeVM)
        {
            ExpenseType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<ExpenseType>().Create(type);
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

        // GET: ExpenseTypes/Edit/1
        public ActionResult Edit(int id)
        {
            return View(ViewModelFromModel(_unitOfWork.Repository<ExpenseType>().Get(id)));
        }

        // POST: ExpenseTypes/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, ExpenseTypeViewModel typeVM)
        {
            ExpenseType type = ModelFromViewModel(typeVM);
            type.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Repository<ExpenseType>().Update(type);
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

        // GET: ExpenseTypes/Delete/1
        public ActionResult Delete(int id)
        {
            return View(CreateDeleteViewModel(id));
        }

        // POST: ExpenseTypes/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var str = collection["DeleteAll"];
                bool delAll = bool.Parse(collection["DeleteAll"].Split(',')[0]);
                var expenses = _unitOfWork.Repository<Expense>().All().Where(ex => ex.ExpenseTypeId == id);
                if (delAll)
                {
                    foreach (var expense in expenses)
                    {
                        _unitOfWork.Repository<Expense>().Delete(expense.Id);
                    }
                    _unitOfWork.Repository<ExpenseType>().Delete(id);
                }
                else
                {
                    int newTypeId = int.Parse(collection["ReplacementTypeId"]);                    
                    foreach(var expense in expenses)
                    {
                        var upd = new Expense { Id = expense.Id, Amount = expense.Amount, Comment = expense.Comment, Date = expense.Date, ExpenseTypeId = newTypeId };
                        _unitOfWork.Repository<Expense>().Update(upd);
                    }
                    _unitOfWork.Repository<ExpenseType>().Delete(id);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(CreateDeleteViewModel(id));
            }
        }

        private DeleteExpenseTypeViewModel CreateDeleteViewModel(int id)
        {
            var type = _unitOfWork.Repository<ExpenseType>().Get(id);
            var replace = _unitOfWork.Repository<ExpenseType>().All()
                .Where(t => t.UserId == type.UserId && t.Id != type.Id)
                .OrderBy(t => t.Name)
                .ToList()
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