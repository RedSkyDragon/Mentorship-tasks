using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ThingsBook.WebAPI.Utils
{
    /// <summary>
    /// Filter for exception handling
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ExceptionFilterAttribute" />
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Called when exception occures.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            string exceptionMessage = string.Empty;
            var logger = log4net.LogManager.GetLogger(GetType());
            logger.Error(context.Exception);
            var statusCode = HttpStatusCode.InternalServerError;
            if (context.Exception.InnerException == null)
            {
                exceptionMessage = context.Exception.Message;
            }
            else
            {
                exceptionMessage = context.Exception.InnerException.Message;
            };
            if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            if (context.Exception is ArgumentException || context.Exception is ArgumentNullException || context.Exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            context.Response = context.Request.CreateErrorResponse(statusCode, exceptionMessage);      
        }
    }
}