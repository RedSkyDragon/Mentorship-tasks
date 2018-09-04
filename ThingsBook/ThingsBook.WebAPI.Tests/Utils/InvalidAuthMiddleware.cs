using IdentityModel;
using Microsoft.Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThingsBook.WebAPI.Tests.Utils
{
    public class InvalidAuthMiddleware: OwinMiddleware
    {
        public InvalidAuthMiddleware(OwinMiddleware next) : base(next) { }

        public async override Task Invoke(IOwinContext context)
        {
            var userId = new Guid("11111111111111111111111111111111");
            var identity = new ClaimsIdentity("TestType");
            var claim = new Claim(JwtClaimTypes.Id, "not Guid");
            identity.AddClaim(claim);
            context.Authentication.User = new ClaimsPrincipal(identity);
            await Next.Invoke(context);
        }
    }
}
