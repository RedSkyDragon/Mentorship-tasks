namespace ThingsBook.IdentityServer.UI
{
    /// <summary>
    /// View model for scope.
    /// </summary>
    public class ScopeViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the emphasize.
        /// </summary>
        public bool Emphasize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this scope is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this scope is checked.
        /// </summary>
        public bool Checked { get; set; }
    }
}
