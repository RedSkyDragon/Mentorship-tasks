using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace IncomeAndExpenses.Web.Utils
{
    public class LoggerMiddleware: OwinMiddleware
    {
        public LoggerMiddleware(OwinMiddleware next) : base(next) { }

        public async override Task Invoke(IOwinContext context)
        {
            var logger = log4net.LogManager.GetLogger(GetType());
            logger.Info("Start");
            await Next.Invoke(context);
            string message = Environment.NewLine + 
                "URI: " + context.Request.Uri + Environment.NewLine +
                "Method: " + context.Request.Method + Environment.NewLine +
                "Status Code: " + context.Response.StatusCode; 
            logger.Info(message);
            logger.Info("Finish");
        }
    }
}