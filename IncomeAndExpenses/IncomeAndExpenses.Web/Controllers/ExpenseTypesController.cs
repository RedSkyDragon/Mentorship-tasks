using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
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
            return View(_unitOfWork.Repository<int, ExpenseType>().Get(id));
        }

        // POST: ExpenseTypes/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.Repository<int, ExpenseType>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(_unitOfWork.Repository<int, ExpenseType>().Get(id));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}