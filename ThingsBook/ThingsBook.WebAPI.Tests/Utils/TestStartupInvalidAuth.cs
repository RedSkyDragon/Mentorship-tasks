using Owin;

namespace ThingsBook.WebAPI.Tests.Utils
{
    /// <summary>
    /// Test startup class for tests for invalid auth.
    /// </summary>
    /// <seealso cref="ThingsBook.WebAPI.Tests.Utils.TestStartup" />
    public class TestStartupInvalidAuth: TestStartup
    {
        /// <summary>
        /// Configures the authentication (invalid auth).
        /// </summary>
        /// <param name="app">The application.</param>
        protected override void ConfigureAuth(IAppBuilder app)
        {
            app.Use<InvalidAuthMiddleware>();
        }
    }
}
