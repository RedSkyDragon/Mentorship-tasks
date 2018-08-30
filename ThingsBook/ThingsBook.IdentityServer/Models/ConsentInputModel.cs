using System.Collections.Generic;

namespace ThingsBook.IdentityServer.UI
{
    /// <summary>
    /// Consent input model
    /// </summary>
    public class ConsentInputModel
    {
        /// <summary>
        /// Gets or sets the button.
        /// </summary>
        public string Button { get; set; }

        /// <summary>
        /// Gets or sets the scopes consented.
        /// </summary>
        public IEnumerable<string> ScopesConsented { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether allow to remember consent.
        /// </summary>
        public bool RememberConsent { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}