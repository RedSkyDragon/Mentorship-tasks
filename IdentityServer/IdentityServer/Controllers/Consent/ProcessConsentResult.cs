namespace IdentityServer.UI
{
    /// <summary>
    /// Model of process consent result.
    /// </summary>
    public class ProcessConsentResult
    {
        /// <summary>
        /// Gets a value indicating whether there is need of redirect.
        /// </summary>
        public bool IsRedirect => RedirectUri != null;

        /// <summary>
        /// Gets or sets the redirect URI.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is need to show view.
        /// </summary>
        public bool ShowView => ViewModel != null;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        public ConsentViewModel ViewModel { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has validation error.
        /// </summary>
        public bool HasValidationError => ValidationError != null;

        /// <summary>
        /// Gets or sets the validation error.
        /// </summary>
        public string ValidationError { get; set; }
    }
}
