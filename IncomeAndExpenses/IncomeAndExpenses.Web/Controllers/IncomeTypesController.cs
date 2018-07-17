using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class IncomeTypesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public IncomeTypesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: IncomeTypes
        public ActionResult Index()
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int,IncomeType>().GetAll().Where(it=>it.UserId == User.Identity.Name));
        }

        // GET: IncomeTypes/Create
        public ActionResult Create()
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View();
        }

        // POST: IncomeTypes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var type = new IncomeType { Name = collection["Name"], Description = collection["Description"], UserId = User.Identity.Name };
            try
            {
                _unitOfWork.Repository<int, IncomeType>().Create(type);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(type);
            }
        }

        // GET: IncomeTypes/Edit/1
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, IncomeType>().Get(id));
        }

        // POST: IncomeTypes/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var type = new IncomeType { Id = id, Name = collection["Name"], Description = collection["Description"], UserId = User.Identity.Name };
            try
            {
                _unitOfWork.Repository<int, IncomeType>().Update(type);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(type);
            }
        }

        // GET: IncomeTypes/Delete/1
        public ActionResult Delete(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
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
                var incomes = _unitOfWork.Repository<int, Income>().GetAll().Where(ex => ex.IncomeTypeId == id);
                if (delAll)
                {
                    foreach (var income in incomes)
                    {
                        _unitOfWork.Repository<int, Income>().Delete(income.Id);
                    }
                    _unitOfWork.Repository<int, IncomeType>().Delete(id);
                }
                else
                {
                    int newTypeId = int.Parse(collection["ReplacementTypeId"]);
                    foreach (var income in incomes)
                    {
                        var upd = new Income { Id = income.Id, Amount = income.Amount, Comment = income.Comment, Date = income.Date, IncomeTypeId = newTypeId };
                        _unitOfWork.Repository<int, Income>().Update(upd);
                    }
                    _unitOfWork.Repository<int, IncomeType>().Delete(id);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(CreateDeleteViewModel(id));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private DeleteIncomeTypeViewModel CreateDeleteViewModel(int id)
        {
            var type = _unitOfWork.Repository<int, IncomeType>().Get(id);
            var replace = _unitOfWork.Repository<int, IncomeType>().GetAll()
                .Where(t => t.UserId == type.UserId && t.Id != type.Id)
                .Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });
            return new DeleteIncomeTypeViewModel { IncomeType = type, ReplacementTypes = replace };
        }
    }
}