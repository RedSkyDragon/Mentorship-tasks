using log4net;
using System.Web.Http;

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
    }
}
