using DotNetOpenAuth.GoogleOAuth2;
using IncomeAndExpenses.Models;
using IncomeAndExpenses.Web.Models;
using Microsoft.AspNet.Membership.OpenAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IncomeAndExpenses.Web.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _unitOfWork;

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
            if (_unitOfWork.Users.Get(authResult.ProviderUserId) == null)
            {
                _unitOfWork.Users.Create(new User { Id = authResult.ProviderUserId, UserName = authResult.UserName });
                _unitOfWork.Save();
            }
            FormsAuthentication.SetAuthCookie(authResult.ProviderUserId, true);
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