using System.Web.Http.ExceptionHandling;
using log4net;

namespace ThingsBook.WebAPI.Infrastructure
{
    /// <summary>
    /// Exception logging
    /// </summary>
    /// <seealso cref="System.Web.Http.ExceptionHandling.ExceptionLogger" />
    public class CustomExceptionLogger: ExceptionLogger
    {
        /// <summary>
        /// When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            var logger = LogManager.GetLogger(GetType());
            logger.Error(context.Exception);
        }
    }
}