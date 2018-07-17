using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
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
            return View(_unitOfWork.Repository<int, IncomeType>().Get(id));
        }

        // POST: IncomeTypes//Delete/1
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _unitOfWork.Repository<int, IncomeType>().Delete(id);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(_unitOfWork.Repository<int, IncomeType>().Get(id));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}