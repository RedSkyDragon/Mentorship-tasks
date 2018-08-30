using System;

namespace ThingsBook.IdentityServer.UI
{
    public class AccountOptions
    {
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = true;

        public static bool AutomaticRedirectAfterSignOut = false;

        public static string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
