using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
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
            ConfigureAuth(app);
            var httpConfiguration = new HttpConfiguration();
            ConfigureAutoFac(httpConfiguration);       
            httpConfiguration.Filters.Add(new CustomExceptionFilter());
            httpConfiguration.MapHttpAttributeRoutes();
            ConfigureSwagger(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

        protected virtual void ConfigureAuth(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost/identityserver",
                RequiredScopes = new[] { "things-book" },
                ClientId = "ThingsBook.WebAPI"
            });
        }

        protected virtual void ConfigureAutoFac(HttpConfiguration httpConfiguration)
        {
            AutofacConfig.ConfigureContainer(httpConfiguration);
        }

        protected virtual void ConfigureSwagger(HttpConfiguration httpConfiguration)
        {
            SwaggerConfig.Register(httpConfiguration);
        }
    }
}
