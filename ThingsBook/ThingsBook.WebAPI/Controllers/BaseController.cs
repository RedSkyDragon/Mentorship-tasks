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

        protected User ApiUser
        {
            get
            {
                var claims = (User as ClaimsPrincipal).Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
                var id = claims.Where(c => c.Type == JwtClaimTypes.Id).FirstOrDefault().Value;
                var name = claims.Where(c => c.Type == JwtClaimTypes.Name).FirstOrDefault().Value;
                return new User { Id = Guid.Parse(id), Name = name };
            }
        }
    }
}
