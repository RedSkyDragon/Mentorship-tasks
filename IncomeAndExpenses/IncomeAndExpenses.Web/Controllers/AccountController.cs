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
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            return Redirect(Url.Action("Index", "Account"));
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            string provider = "Google";
            string returnUrl = "";
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "~/";
            }
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
                _unitOfWork.Repository<IncomeType>().Create(new IncomeType { UserId = userId, Name = "Other", Description = "Income that are difficult to classify as specific type." });
                _unitOfWork.Repository<ExpenseType>().Create(new ExpenseType { UserId = userId, Name = "Other", Description = "Expense that are difficult to classify as specific type." });
                _unitOfWork.Save();
            }
            IdentitySignin(userId, name, isPersistent: true);
            return Redirect(returnUrl);
        }

        private void IdentitySignin(string userId, string name, bool isPersistent = false)
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}