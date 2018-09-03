using IdentityModel;
using Microsoft.Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThingsBook.WebAPI.Tests.Utils
{
    /// <summary>
    /// Authentication middleware
    /// </summary>
    /// <seealso cref="Microsoft.Owin.OwinMiddleware" />
    class AuthMiddleware : OwinMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        public AuthMiddleware(OwinMiddleware next) : base(next) { }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
