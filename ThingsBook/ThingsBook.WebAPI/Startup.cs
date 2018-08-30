﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using ThingsBook.WebAPI.Utils;

namespace ThingsBook.WebAPI
{
    /// <summary>
    /// Startup class for api project
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {     
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost/thingsbook.identityserver",
                RequiredScopes = new[] { "things-book" }
                //AuthenticationType = "Bearer",
                //ValidationMode = ValidationMode.Local
                //ClientId = "ThingsBook.WebAPI",
                //ClientSecret = "secret"
            });
            var httpConfiguration = new HttpConfiguration();
            AutofacConfig.ConfigureContainer(httpConfiguration);            
            httpConfiguration.Filters.Add(new CustomExceptionFilter());
            httpConfiguration.MapHttpAttributeRoutes();
            SwaggerConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}