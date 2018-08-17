using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;
using ThingsBook.Data.Mongo;

namespace ThingsBook.WebAPI.Utils
{
    /// <summary>
    /// Configures AutoFac
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// configures AutoFac options for current project
        /// </summary>
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => new ThingsBookContext()).As<ThingsBookContext>().InstancePerRequest();
            RegisterDAL(builder);
            RegisterBL(builder);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterDAL(ContainerBuilder buider)
        {
            buider.RegisterType<Users>().As<IUsers>();
            buider.RegisterType<Friends>().As<IFriends>();
            buider.RegisterType<Categories>().As<ICategories>();
            buider.RegisterType<Things>().As<IThings>();
            buider.RegisterType<Lends>().As<ILends>();
            buider.RegisterType<History>().As<IHistory>();
            buider.RegisterType<CommonDAL>().As<CommonDAL>();
        }

        private static void RegisterBL(ContainerBuilder buider)
        {
            buider.RegisterType<UsersBL>().As<IUsersBL>();
            buider.RegisterType<FriendsBL>().As<IFriendsBL>();
            buider.RegisterType<ThingsBL>().As<IThingsBL>();
            buider.RegisterType<LendsBL>().As<ILendsBL>();
        }
    }
}