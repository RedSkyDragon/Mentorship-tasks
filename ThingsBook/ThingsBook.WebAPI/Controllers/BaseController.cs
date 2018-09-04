using IdentityModel;
using log4net;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Controllers
{
    /// <summary>
    /// Base for controllers with logger
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BaseController : ApiController
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILog Logger { get { return LogManager.GetLogger(GetType()); } }

        /// <summary>
        /// Gets the API user.
        /// </summary>
        /// <exception cref="ThingsBook.WebAPI.Models.UserClaimsException">
        /// </exception>
        protected User ApiUser
        {
            get
            {
                var claims = (User as ClaimsPrincipal).Claims.Select(c => new { c.Type, c.Value }).ToList();
                var idClaim = claims.Where(c => c.Type == JwtClaimTypes.Id);
                var nameClaim = claims.Where(c => c.Type == JwtClaimTypes.Name);               
                if (idClaim.Count() == 0 || nameClaim.Count() == 0)
                {
                    throw new Models.UserClaimsException();
                }
                Guid id;
                var name = nameClaim.FirstOrDefault().Value;
                if (!Guid.TryParse(idClaim.FirstOrDefault().Value, out id) || string.IsNullOrEmpty(name))
                {
                    throw new Models.UserClaimsException();
                }
                return new User { Id = id, Name = name };
            }
        }
    }
}
