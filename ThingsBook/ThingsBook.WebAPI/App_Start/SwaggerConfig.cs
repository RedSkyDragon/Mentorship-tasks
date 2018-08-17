using System.Web.Http;
using WebActivatorEx;
using ThingsBook.WebAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace ThingsBook.WebAPI
{
    /// <summary>
    /// Swagger configuration.
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "ThingsBook.WebAPI");
                        c.PrettyPrint();
                        c.IncludeXmlComments(string.Format(@"{0}\bin\ThingsBook.WebAPI.XML",
                           System.AppDomain.CurrentDomain.BaseDirectory)); 
                        c.UseFullTypeNameInSchemaIds();
                        // In contrast to WebApi, Swagger 2.0 does not include the query string component when mapping a URL
                        // to an action. As a result, Swashbuckle will raise an exception if it encounters multiple actions
                        // with the same path (sans query string) and HTTP method. You can workaround this by providing a
                        // custom strategy to pick a winner or merge the descriptions for the purposes of the Swagger docs
                        //
                        //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    })
                .EnableSwaggerUi();
        }
    }
}
