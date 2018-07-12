using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IncomeAndExpenses.Startup))]
namespace IncomeAndExpenses
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
