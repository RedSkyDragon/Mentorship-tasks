using System.Web.Http;
using ThingsBook.WebAPI.Utils;

namespace ThingsBook.WebAPI
{
    /// <summary>
    /// Application class
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Applications start function.
        /// </summary>
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
