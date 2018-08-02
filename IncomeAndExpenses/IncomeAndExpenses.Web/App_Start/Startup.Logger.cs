using IncomeAndExpenses.Web.Utils;
using Owin;

namespace IncomeAndExpenses.Web
{
    public partial class Startup
    {
        /// <summary>
        /// Configure logging using OWIN and log4net
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureLogger(IAppBuilder app)
        {
            app.Use<LoggerMiddleware>();
        }
    }
}