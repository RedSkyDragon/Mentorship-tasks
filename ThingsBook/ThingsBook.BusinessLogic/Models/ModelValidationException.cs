using System;

namespace ThingsBook.BusinessLogic.Models
{
    /// <summary>
    /// Class for model validation exceptions.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ModelValidationException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ModelValidationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValidationException"/> class.
        /// </summary>
        public ModelValidationException() : this("Model is not valid.") { }
    }
}
