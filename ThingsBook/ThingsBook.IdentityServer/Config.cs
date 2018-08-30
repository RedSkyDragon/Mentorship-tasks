﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ThingsBook.IdentityServer
{
    /// <summary>
    /// Basic configuration for identity server.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets the API resources.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "ThingsBook.WebAPI",
                    DisplayName = "ThingsBook",
                    //ApiSecrets = { new Secret("secret".Sha256()) },
                    UserClaims = new List<string> { JwtClaimTypes.Id, JwtClaimTypes.Name },
                    Scopes = { new Scope
                    {
                        Name = "things-book",
                        Required = true,
                        ShowInDiscoveryDocument = true
                    }}
                }
            };
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the identity resources.
        /// </summary>
        /// <returns></returns>
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