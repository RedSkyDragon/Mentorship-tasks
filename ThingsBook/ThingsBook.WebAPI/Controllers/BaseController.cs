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

        protected string UserName
        {
            get
            {
                var claims = (User as ClaimsPrincipal).Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
                return claims.Where(c => c.Type == "user_name").FirstOrDefault().Value;
            }
        }
        protected string UserId
        {
            get
            {
                var claims = (User as ClaimsPrincipal).Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();               
                return claims.Where(c => c.Type == "user_id").FirstOrDefault().Value;
            }
        }
    }
}
