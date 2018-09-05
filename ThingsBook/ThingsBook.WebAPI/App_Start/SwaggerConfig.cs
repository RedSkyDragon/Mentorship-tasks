using System.Web.Http;
using Swashbuckle.Application;

namespace ThingsBook.WebAPI
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            config
                .EnableSwagger(c =>
                {
                    c.RootUrl(rec => "http://localhost/ThingsBook.WebAPI");
                    c.SingleApiVersion("v1", "ThingsBook.WebAPI");
                    c.PrettyPrint();
                    c.IncludeXmlComments(string.Format(@"{0}\bin\ThingsBook.WebAPI.XML",
                       System.AppDomain.CurrentDomain.BaseDirectory));
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi(c => { });
        }
    }
}
