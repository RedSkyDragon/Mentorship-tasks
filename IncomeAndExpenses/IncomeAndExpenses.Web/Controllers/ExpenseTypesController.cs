using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    [Authorize]
    public class ExpenseTypesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ExpenseTypesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: ExpenseTypes
        public ActionResult Index()
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, ExpenseType>().GetAll().Where(it => it.UserId == User.Identity.Name));
        }

        // GET: ExpenseTypes/Create
        public ActionResult Create()
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View();
        }

        // POST: ExpenseTypes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var type = new ExpenseType { Name = collection["Name"], Description = collection["Description"], UserId = User.Identity.Name };
            try
            {
                _unitOfWork.Repository<int, ExpenseType>().Create(type);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(type);
            }
        }

        // GET: ExpenseTypes/Edit/1
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
            return View(_unitOfWork.Repository<int, ExpenseType>().Get(id));
        }

        // POST: ExpenseTypes/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var type = new ExpenseType { Id = id, Name = collection["Name"], Description = collection["Description"], UserId = User.Identity.Name };
            try
            {
                _unitOfWork.Repository<int, ExpenseType>().Update(type);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(type);
            }
        }

        // GET: ExpenseTypes/Delete/1
        public ActionResult Delete(int id)
        {
            ViewBag.UserName = _unitOfWork.Repository<string, User>().Get(User.Identity.Name).UserName;
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
                var expenses = _unitOfWork.Repository<int, Expense>().GetAll().Where(ex => ex.ExpenseTypeId == id);
                if (delAll)
                {
                    foreach (var expense in expenses)
                    {
                        _unitOfWork.Repository<int, Expense>().Delete(expense.Id);
                    }
                    _unitOfWork.Repository<int, ExpenseType>().Delete(id);
                }
                else
                {
                    int newTypeId = int.Parse(collection["ReplacementTypeId"]);                    
                    foreach(var expense in expenses)
                    {
                        var upd = new Expense { Id = expense.Id, Amount = expense.Amount, Comment = expense.Comment, Date = expense.Date, ExpenseTypeId = newTypeId };
                        _unitOfWork.Repository<int, Expense>().Update(upd);
                    }
                    _unitOfWork.Repository<int, ExpenseType>().Delete(id);
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

        private DeleteExpenseTypeViewModel CreateDeleteViewModel(int id)
        {
            var type = _unitOfWork.Repository<int, ExpenseType>().Get(id);
            var replace = _unitOfWork.Repository<int, ExpenseType>().GetAll()
                .Where(t => t.UserId == type.UserId && t.Id != type.Id)
                .Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() });
            return new DeleteExpenseTypeViewModel { ExpenseType = type, ReplacementTypes = replace };
        }
    }
}