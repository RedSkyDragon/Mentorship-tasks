using System.Collections.Generic;

namespace IdentityServer.Models
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
}