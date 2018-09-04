using System.Collections.Generic;

namespace IdentityServer.UI
{
    /// <summary>
    /// Consent view model.
    /// </summary>
    /// <seealso cref="IdentityServer.UI.ConsentInputModel" />
    public class ConsentViewModel : ConsentInputModel
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        public string ClientUrl { get; set; }

        /// <summary>
        /// Gets or sets the client logo URL.
        /// </summary>
        public string ClientLogoUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether allow remember consent.
        /// </summary>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// Gets or sets the identity scopes.
        /// </summary>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

        /// <summary>
        /// Gets or sets the resource scopes.
        /// </summary>
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
    }
}
