using IdentityModel;
using log4net;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Base for controllers with logger
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILog Logger => LogManager.GetLogger(GetType());

        /// <summary>
        /// Gets the API user.
        /// </summary>
        /// <exception cref="ThingsBook.WebAPI.Models.UserClaimsException">
        /// </exception>
        protected User ApiUser
        {
            get
            {
                if (!(User is ClaimsPrincipal claimsPrincipal))
                {
                    throw new Models.UserClaimsException();
                }
                var claims = claimsPrincipal.Claims.Select(c => new { c.Type, c.Value }).ToList();
                var idClaim = claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Id);
                var nameClaim = claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name);               
                if (idClaim == null || nameClaim == null)
                {
                    throw new Models.UserClaimsException();
                }
                var name = nameClaim.Value;
                if (!Guid.TryParse(idClaim.Value, out var id) || string.IsNullOrEmpty(name))
                {
                    throw new Models.UserClaimsException();
                }
                return new User { Id = id, Name = name };
            }
        }
    }
}
