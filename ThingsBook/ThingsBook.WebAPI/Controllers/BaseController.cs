using log4net;
using System.Web.Http;

namespace ThingsBook.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected ILog Logger { get { return LogManager.GetLogger(GetType()); } }
    }
}
