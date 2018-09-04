using Owin;

namespace ThingsBook.WebAPI.Tests.Utils
{
    public class TestStartupWithoutAuth: TestStartup
    {
        /// <summary>
        /// Configures the authentication (no auth).
        /// </summary>
        /// <param name="app">The application.</param>
        protected override void ConfigureAuth(IAppBuilder app) { }
    }
}
