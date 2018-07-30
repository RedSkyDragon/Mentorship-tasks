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
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access.";
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
            }
            else
            {
                message = context.Exception.Message;
            }
            context.ExceptionHandled = true;
            logger.Error(message + " " + context.Exception.StackTrace);
            var dataDictionary = new ViewDataDictionary();
            dataDictionary.Add("message", message);
            context.Result = new ViewResult { ViewName = "~/Views/Error/Index.cshtml", ViewData = dataDictionary};
            context.ExceptionHandled = true;
        }
    }
}