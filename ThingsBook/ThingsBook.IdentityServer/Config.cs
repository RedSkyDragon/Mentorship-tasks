using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThingsBook.IdentityServer
{
    public class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Alex",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim("user_name", "Alex", "string"),
                        new Claim("user_id", "user_idForAlex", "Guid")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim("user_name", "Bob"),
                        new Claim("user_id", "user_idForBob")
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "ThingsBook.WebAPI",
                    DisplayName = "ThingsBook",
                    //ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims = new List<string> { "user_name", "user_id" },
                    Scopes = { new Scope
                    {
                        Name = "things-book",
                        Required = true,
                        ShowInDiscoveryDocument = true
                    }}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "MVCClient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost/ThingsBook.MVCClient/Home/Callback" },
                    PostLogoutRedirectUris = { "http://localhost/ThingsBook.MVCClient" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "things-book"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
        }
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
