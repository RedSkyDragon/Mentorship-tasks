using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.BusinessLogic;
using IncomeAndExpenses.Web.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Controllers
{
    public class AccountController : BaseController
    {
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }

        /// <summary>
        /// Creates controller with IBusinessLogic instance
        /// </summary>
        /// <param name="businessLogic">IBusinessLogic implementation to work with data</param>
        public AccountController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        //GET Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //GET Account/LogOff
        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            return Redirect(Url.Action("Index", "Account"));
        }

        //GET Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            string provider = "Google";
            string returnUrl = string.Empty;
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [HttpGet]
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
            if (_businessLogic.GetUser(userId) == null)
            {
                _businessLogic.CreateUser(new User { Id = userId, UserName = name });
            }
            else
            {
                _businessLogic.UpdateUser(new User { Id = userId, UserName = name });
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
    }
}