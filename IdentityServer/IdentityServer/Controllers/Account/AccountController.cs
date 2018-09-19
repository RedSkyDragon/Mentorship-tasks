using System.Linq;
using System.Security.Claims;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Extensions;
using IdentityServer.Models;
using IdentityServer.Utils;

namespace IdentityServer.Controllers
{
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _events = events;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var vm = BuildRegisterViewModel(returnUrl);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string button)
        {
            if (button != "login")
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                    return Redirect(model.ReturnUrl);
                }
                return Redirect("~/");
            }
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return Redirect("~/");
                }
                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string button)
        {
            if (button != "register")
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                    return Redirect(model.ReturnUrl);
                }
                return Redirect("~/");
            }
            if (model.Password != model.RepeatPassword)
            {
                ModelState.AddModelError("RepeatPassword", "Wrong password repeat");
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    model.Name = model.Username;
                }
                var user = _userManager.FindByNameAsync(model.Username).Result;
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = model.Username
                    };
                    var result = _userManager.CreateAsync(user, model.Password).Result;
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("Data", result.Errors.First().Description);
                    }
                    else
                    {
                        result = _userManager.AddClaimsAsync(user, new[]
                        {
                            new Claim(JwtClaimTypes.Id, SequentialGuidUtils.CreateGuid().ToString()),
                            new Claim(JwtClaimTypes.Name, model.Name)
                        }).Result;
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("Data", result.Errors.First().Description);
                        }
                        return RedirectToAction("Registered", new { returnUrl = model.ReturnUrl });
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "This username is already exists");
                }
            }
            var vm = BuildRegisterViewModel(model);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Registered(string returnUrl)
        {
            var vm = new RegisteredViewModel { ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLogoutViewModelAsync(logoutId);
            if (vm.ShowLogoutPrompt == false)
            {
                return await Logout(vm);
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutViewModel model)
        {
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);
            if (User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            return View("LoggedOut", vm);
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Username = context?.LoginHint
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private RegisterViewModel BuildRegisterViewModel(string returnUrl)
        {
            return new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
        }

        private RegisterViewModel BuildRegisterViewModel(RegisterViewModel model)
        {
            return new RegisterViewModel
            {
                ReturnUrl = model.ReturnUrl,
                Name = model.Name,
                Username = model.Username
            };
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };
            if (User?.Identity.IsAuthenticated != true)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout.ClientName,
                LogoutId = logoutId
            };
            return vm;
        }
    }
}
