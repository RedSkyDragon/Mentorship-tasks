using System;
using System.Collections.Generic;

namespace IdentityServer.UI
{
    /// <summary>
    /// Grants view model.
    /// </summary>
    public class GrantsViewModel
    {
        /// <summary>
        /// Gets or sets the grants.
        /// </summary>
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }

    /// <summary>
    /// Grant view model
    /// </summary>
    public class GrantViewModel
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string ClientId { get; set; }

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
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        public DateTime? Expires { get; set; }

        /// <summary>
        /// Gets or sets the identity grant names.
        /// </summary>
        public IEnumerable<string> IdentityGrantNames { get; set; }

        /// <summary>
        /// Gets or sets the API grant names.
        /// </summary>
        public IEnumerable<string> ApiGrantNames { get; set; }
    }
}