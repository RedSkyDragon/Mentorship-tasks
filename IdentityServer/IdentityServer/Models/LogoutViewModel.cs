namespace IdentityServer.UI
{
    /// <summary>
    /// Logout View model
    /// </summary>
    public class LogoutViewModel
    {
        /// <summary>
        /// Gets or sets the logout identifier.
        /// </summary>
        public string LogoutId { get; set; }

        /// <summary>
        /// Gets or sets a value of showing logout prompt.
        /// </summary>
        public bool ShowLogoutPrompt { get; set; }
    }
}
