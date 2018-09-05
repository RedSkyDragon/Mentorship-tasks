using System;
using System.Net;
using System.Web.Http.ExceptionHandling;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Models;

namespace ThingsBook.WebAPI.Infrastructure
{
    /// <summary>
    /// Exception Handling
    /// </summary>
    /// <seealso cref="System.Web.Http.ExceptionHandling.ExceptionHandler" />
    public class CustomExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// When overridden in a derived class, handles the exception synchronously.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var exceptionMessage = context.Exception.InnerException == null ? context.Exception.Message : context.Exception.InnerException.Message;
            if (context.Exception is UnauthorizedAccessException ||
                context.Exception is UserClaimsException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            if (context.Exception is ModelValidationException ||
                context.Exception is ArgumentException ||
                context.Exception is ArgumentNullException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            context.Result = new ErrorResult {StatusCode = statusCode, Message = exceptionMessage};
        }
    }
}