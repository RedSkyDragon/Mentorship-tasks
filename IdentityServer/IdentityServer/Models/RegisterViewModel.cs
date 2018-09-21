using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    /// <summary>
    /// View model for register page.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the repeat password.
        /// </summary>
        [Required]
        public string RepeatPassword { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
