using System;

namespace ThingsBook.WebAPI.Models
{
    /// <summary>
    /// Class for exception of user claims.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UserClaimsException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UserClaimsException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsException"/> class.
        /// </summary>
        public UserClaimsException() : this("User information must contains valid id (Guid) and name claims.") { }
    }
}