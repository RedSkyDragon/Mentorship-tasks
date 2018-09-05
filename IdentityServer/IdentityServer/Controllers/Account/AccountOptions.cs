using System;

namespace IdentityServer.Controllers
{
    /// <summary>
    /// Account options for account controller
    /// </summary>
    public class AccountOptions
    {
        /// <summary>
        /// The login memorization duration
        /// </summary>
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        /// <summary>
        /// The showing of logout prompt
        /// </summary>
        public static bool ShowLogoutPrompt = true;

        /// <summary>
        /// The automatic redirect after sign out
        /// </summary>
        public static bool AutomaticRedirectAfterSignOut = false;

        /// <summary>
        /// The invalid credentials error message
        /// </summary>
        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
