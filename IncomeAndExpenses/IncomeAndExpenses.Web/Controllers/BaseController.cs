using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IncomeAndExpenses.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _unitOfWork;
        protected string UserId { get { return User.Identity.GetUserId(); } }

        protected log4net.ILog Logger { get { return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); } }

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaseController() : this(null) { }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}