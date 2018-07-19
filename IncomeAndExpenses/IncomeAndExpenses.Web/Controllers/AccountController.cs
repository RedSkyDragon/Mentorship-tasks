using DotNetOpenAuth.GoogleOAuth2;
using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Membership.OpenAuth;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
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
        
        [AllowAnonymous]
        public void IdentitySignin(string userId, string name, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            claims.Add(new Claim(ClaimTypes.Name, name));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                          DefaultAuthenticationTypes.ExternalCookie);
            return Redirect(Url.Action("Index", "Account"));
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            string provider = "Google";
            string returnUrl = "";
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback",
                                       "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "~/";

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return Redirect(Url.Action("Index", "Account"));
            }
            var userId = loginInfo.Login.ProviderKey;
            var name = loginInfo.ExternalIdentity.Name;
            if (_unitOfWork.Repository<string, User>().Get(userId) == null)
            {
                _unitOfWork.Repository<string, User>().Create(new User { Id = userId, UserName = name });
                _unitOfWork.Repository<int, IncomeType>().Create(new IncomeType { UserId = userId, Name = "Other", Description = "Income that are difficult to classify as specific type." });
                _unitOfWork.Repository<int, ExpenseType>().Create(new ExpenseType { UserId = userId, Name = "Other", Description = "Expense that are difficult to classify as specific type." });
                _unitOfWork.Save();
            }

            IdentitySignin(userId, name, isPersistent: true);

            return Redirect(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}