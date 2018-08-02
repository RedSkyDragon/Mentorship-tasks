using System;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    /// <summary>
    /// Exception filter which shows custom exception page and loggers information about exceptions
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Actions when exception occurs
        /// </summary>
        /// <param name="context">The exception context</param>
        public void OnException(ExceptionContext context)
        {
            String message = String.Empty;
            var logger = log4net.LogManager.GetLogger(GetType());
            var exceptionType = context.Exception.GetType();
            if (context.Exception is UnauthorizedAccessException)
            {
                message = "Unauthorized Access.";
            }
            else if (context.Exception is NotImplementedException)
            {
                message = "This feature will be implemented later.";
            }
            else
            {
                message = "A server error occurred.";
            };
            logger.Error(message, context.Exception);
            var dataDictionary = new ViewDataDictionary();
            dataDictionary.Add("message", message);
            context.Result = new ViewResult { ViewName = "~/Views/Error/Index.cshtml", ViewData = dataDictionary};
            context.ExceptionHandled = true;
        }
    }
}