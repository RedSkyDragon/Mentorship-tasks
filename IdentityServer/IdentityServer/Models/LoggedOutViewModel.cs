namespace IdentityServer.Models
{
    /// <summary>
    /// Logged out view model.
    /// </summary>
    public class LoggedOutViewModel
    {
        /// <summary>
        /// Gets or sets the post logout redirect URI.
        /// </summary>
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether need to automatic redirect after sign out.
        /// </summary>
        public bool AutomaticRedirectAfterSignOut { get; set; }

        /// <summary>
        /// Gets or sets the logout identifier.
        /// </summary>
        public string LogoutId { get; set; }
    }
}