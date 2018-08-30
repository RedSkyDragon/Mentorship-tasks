namespace ThingsBook.MVCClient.Models
{
    /// <summary>
    /// View model for error page
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is need of showing request identifier.
        /// </summary>
        /// <value>
        /// <c>true</c> if request identifier is not null or empty; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}