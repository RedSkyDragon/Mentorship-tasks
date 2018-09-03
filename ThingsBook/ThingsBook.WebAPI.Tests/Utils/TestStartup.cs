using System.Web.Http;
using Owin;

namespace ThingsBook.WebAPI.Tests.Utils
{
    /// <summary>
    /// Test startup class
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Startup" />
    public class TestStartup: Startup
    {
        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        protected override void ConfigureAuth(IAppBuilder app)
        {
            app.Use<AuthMiddleware>();
        }

        /// <summary>
        /// Configures dependency injections.
        /// </summary>
        /// <param name="httpConfiguration">The HTTP configuration.</param>
        protected override void ConfigureDI(HttpConfiguration httpConfiguration)
        {
            TestAutofac.ConfigureContainer(httpConfiguration);
        }

        /// <summary>
        /// Configures the swagger.
        /// </summary>
        /// <param name="httpConfiguration">The HTTP configuration.</param>
        protected override void ConfigureSwagger(HttpConfiguration httpConfiguration) { }
    }
}
