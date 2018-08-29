using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Security.Claims;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using IdentityModel;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using ThingsBook.MVCClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MvcImplicit.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(string token)
        {
            if (token == null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return View();
                }
                return RedirectToAction("Login");
            }
            var user = await GetUserInfoFromApi(token);
            if (user == null)
            {
                user = await CreateApiUser(token);
            }
            var model = new IndexViewModel
            {
                ApiUser = user,
                Categories = await GetCategoriesInfoFromApi(token),
                Things = await GetThingsInfoFromApi(token)
            };
            return View(model);
        }

        public Task<IActionResult> Login()
        {
            return StartAuthentication();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task<IActionResult> StartAuthentication()
        {
            var client = new DiscoveryClient("http://localhost/thingsbook.identityserver");
            client.Policy.RequireHttps = false;
            var disco = await client.GetAsync();
            var authorizeUrl = new RequestUrl(disco.AuthorizeEndpoint).CreateAuthorizeUrl(
                clientId: "MVCClient",
                responseType: "id_token token",
                scope: "things-book openid profile",
                redirectUri: "http://localhost/ThingsBook.MVCClient/Home/Callback",
                state: "random_state",
                nonce: "random_nonce",
                responseMode: "form_post",
                idTokenHint: "");
            return Redirect(authorizeUrl);
        }

        public async Task<IActionResult> Callback()
        {
            var state = Request.Form["state"].FirstOrDefault();
            var idToken = Request.Form["id_token"].FirstOrDefault();
            var accessToken = Request.Form["access_token"].FirstOrDefault();
            var error = Request.Form["error"].FirstOrDefault();
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            if (!string.Equals(state, "random_state"))
            {
                throw new Exception("Invalid state");
            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", new { token = accessToken });
            }
            var user = await ValidateIdentityToken(idToken);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
            return RedirectToAction("Index", new { token = accessToken });
        }

        public async Task<IActionResult> Logout()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost/thingsbook.identityserver");
            var endSessionUrl = new RequestUrl(disco.EndSessionEndpoint).CreateEndSessionUrl(
                idTokenHint: "",
                postLogoutRedirectUri: "");
            return Redirect(endSessionUrl);
        }

        private async Task<User> GetUserInfoFromApi(string token)
        {
            string apiUrl = "http://localhost/ThingsBook.WebAPI/user";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.SetBearerToken(token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<User>(data);
                }
                return null;                
            }
        }

        private async Task<User> CreateApiUser(string token)
        {
            string apiUrl = "http://localhost/ThingsBook.WebAPI/user";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.SetBearerToken(token);

                HttpResponseMessage response = await client.PostAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<User>(data);
                }
                return null;
            }
        }

        private async Task<IEnumerable<Category>> GetCategoriesInfoFromApi(string token)
        {
            string apiUrl = "http://localhost/ThingsBook.WebAPI/categories";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.SetBearerToken(token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Category>>(data);
                }
                return null;
            }
        }

        private async Task<IEnumerable<Thing>> GetThingsInfoFromApi(string token)
        {
            string apiUrl = "http://localhost/ThingsBook.WebAPI/things";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.SetBearerToken(token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Thing>>(data);
                }
                return null;
            }
        }

        private async Task<ClaimsPrincipal> ValidateIdentityToken(string idToken)
        {
            var user = await ValidateJwt(idToken);
            var nonce = user.FindFirst("nonce")?.Value ?? "";
            if (!string.Equals(nonce, "random_nonce")) throw new Exception("invalid nonce");
            return user;
        }

        private async Task<ClaimsPrincipal> ValidateLogoutToken(string logoutToken)
        {
            var claims = await ValidateJwt(logoutToken);

            if (claims.FindFirst("sub") == null && claims.FindFirst("sid") == null) throw new Exception("Invalid logout token");

            var nonce = claims.FindFirstValue("nonce");
            if (!String.IsNullOrWhiteSpace(nonce)) throw new Exception("Invalid logout token");

            var eventsJson = claims.FindFirst("events")?.Value;
            if (String.IsNullOrWhiteSpace(eventsJson)) throw new Exception("Invalid logout token");

            var events = JObject.Parse(eventsJson);
            var logoutEvent = events.TryGetValue("http://schemas.openid.net/event/backchannel-logout");
            if (logoutEvent == null) throw new Exception("Invalid logout token");

            return claims;
        }

        private static async Task<ClaimsPrincipal> ValidateJwt(string jwt)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost/thingsbook.identityserver");
            var keys = new List<SecurityKey>();
            foreach (var webKey in disco.KeySet.Keys)
            {
                var e = Base64Url.Decode(webKey.E);
                var n = Base64Url.Decode(webKey.N);
                var key = new RsaSecurityKey(new RSAParameters { Exponent = e, Modulus = n })
                {
                    KeyId = webKey.Kid
                };
                keys.Add(key);
            }
            var parameters = new TokenValidationParameters
            {
                ValidIssuer = disco.Issuer,
                ValidAudience = "MVCClient",
                IssuerSigningKeys = keys,
                NameClaimType = JwtClaimTypes.Name,
                RoleClaimType = JwtClaimTypes.Role
            };
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();
            var user = handler.ValidateToken(jwt, parameters, out var validatedToken);
            return user;
        }
    }
}