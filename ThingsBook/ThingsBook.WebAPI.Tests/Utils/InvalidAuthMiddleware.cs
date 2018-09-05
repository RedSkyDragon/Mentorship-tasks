using IdentityModel;
using Microsoft.Owin;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThingsBook.WebAPI.Tests.Utils
{
    public class InvalidAuthMiddleware: OwinMiddleware
    {
        public InvalidAuthMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            var identity = new ClaimsIdentity("TestType");
            var claim = new Claim(JwtClaimTypes.Id, "not Guid");
            identity.AddClaim(claim);
            context.Authentication.User = new ClaimsPrincipal(identity);
            await Next.Invoke(context);
        }
    }
}
