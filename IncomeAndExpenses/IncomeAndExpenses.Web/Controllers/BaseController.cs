using IncomeAndExpenses.DataAccessInterface;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IncomeAndExpenses.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// IUnitOfWork to connect with database
        /// </summary>
        protected IUnitOfWork _unitOfWork;

        protected string UserId { get { return User.Identity.GetUserId(); } }

        protected log4net.ILog Logger { get { return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); } }

        /// <summary>
        /// Creates controller with UnitOfWork instance to connect with database
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork implementation to connect with database</param>
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates controller without UnitOfWork instance to connect with database
        /// </summary>
        public BaseController() : this(null) { }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}