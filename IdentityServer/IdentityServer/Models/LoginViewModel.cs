using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
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
        /// Gets or sets a value indicating whether need to remember login.
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}