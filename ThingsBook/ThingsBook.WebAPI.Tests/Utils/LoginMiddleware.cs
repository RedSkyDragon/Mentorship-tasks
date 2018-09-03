using IdentityModel;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ThingsBook.WebAPI.Tests.Utils
{
    class LoginMiddleware : OwinMiddleware
    {
        public LoginMiddleware(OwinMiddleware next) : base(next) { }

        public async override Task Invoke(IOwinContext context)
        {
            var userId = new Guid("11111111111111111111111111111111");
            var identity = new ClaimsIdentity("TestType");
            var claims = new Claim[]
            {
                new Claim(JwtClaimTypes.Id, userId.ToString()),
                new Claim(JwtClaimTypes.Name, "UserName")
            };
            identity.AddClaims(claims);
            context.Authentication.User = new ClaimsPrincipal(identity);
            await Next.Invoke(context);
        }
    }
}
