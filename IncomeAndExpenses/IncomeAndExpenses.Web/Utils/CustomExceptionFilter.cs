using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            String message = String.Empty;
            var logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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