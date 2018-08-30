namespace ThingsBook.IdentityServer.UI
{
    public class LoggedOutViewModel
    {
        public string PostLogoutRedirectUri { get; set; }

        public string ClientName { get; set; }

        public bool AutomaticRedirectAfterSignOut { get; set; }

        public string LogoutId { get; set; }
    }
}