﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IncomeAndExpenses.Web.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace IncomeAndExpenses.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureLogger(app);
            ConfigureAuth(app);
        }
    }
}
