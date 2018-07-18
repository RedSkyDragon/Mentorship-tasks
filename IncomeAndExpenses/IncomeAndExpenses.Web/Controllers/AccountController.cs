using DotNetOpenAuth.GoogleOAuth2;
using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Web.Mvc;
using System.Web.Security;

namespace IncomeAndExpenses.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AccountController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginGoogle()
        {
            string provider = "google";
            string returnUrl = "";
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            GoogleOAuth2Client.RewriteRequest();
            var redirectUrl = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });
            var authResult = OpenAuth.VerifyAuthentication(redirectUrl);
            if (!authResult.IsSuccessful)
            {
                return Redirect(Url.Action("Index", "Account"));
            }
            if (_unitOfWork.Repository<string, User>().Get(authResult.ProviderUserId) == null)
            {
                _unitOfWork.Repository<string, User>().Create(new User { Id = authResult.ProviderUserId, UserName = authResult.UserName });
                _unitOfWork.Repository<int, IncomeType>().Create(new IncomeType { UserId = authResult.ProviderUserId, Name = "Other", Description = "Income that are difficult to classify as specific type." });
                _unitOfWork.Repository<int, ExpenseType>().Create(new ExpenseType { UserId = authResult.ProviderUserId, Name = "Other", Description = "Expense that are difficult to classify as specific type." });
                _unitOfWork.Save();
            }
            
            FormsAuthentication.SetAuthCookie(string.Join("|", authResult.ProviderUserId, authResult.UserName), true);
            return Redirect(Url.Action("Index", "Home"));
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Index", "Account"));
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}