using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using IncomeAndExpenses.BusinessLogic;

namespace IncomeAndExpenses.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string UserId { get { return User.Identity.GetUserId(); } }

        protected log4net.ILog Logger { get { return log4net.LogManager.GetLogger(GetType()); } }

        protected const string ErrorMessage = "Sorry, something went wrong. Please try again and be sure that all fields are correct.";
    }
}