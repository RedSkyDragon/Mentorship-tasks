using System.Web.Http;
using Owin;

namespace ThingsBook.WebAPI.Tests.Utils
{
    public class TestStartup: Startup
    {
        protected override void ConfigureAuth(IAppBuilder app)
        {
            app.Use<LoginMiddleware>();
        }

        protected override void ConfigureAutoFac(HttpConfiguration httpConfiguration)
        {
            TestAutofac.ConfigureContainer(httpConfiguration);
        }

        protected override void ConfigureSwagger(HttpConfiguration httpConfiguration) { }
    }
}
