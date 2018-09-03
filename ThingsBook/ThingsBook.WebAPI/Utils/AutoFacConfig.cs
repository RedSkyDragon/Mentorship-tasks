using Autofac;
using Autofac.Integration.WebApi;
using MongoDB.Driver;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;
using ThingsBook.Data.Mongo;

namespace ThingsBook.WebAPI.Utils
{
    /// <summary>
    /// AutoFac configuration.
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// configures AutoFac options for current project
        /// </summary>
        public static void ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<MongoClient>().As<IMongoClient>();
            builder.RegisterType<ThingsBookContext>().AsSelf()
                .WithParameter("connectionString", ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString)
                .SingleInstance();
            RegisterDAL(builder);
            RegisterBL(builder);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterDAL(ContainerBuilder buider)
        {
            buider.RegisterType<UsersDAL>().As<IUsersDAL>();
            buider.RegisterType<FriendsDAL>().As<IFriendsDAL>();
            buider.RegisterType<CategoriesDAL>().As<ICategoriesDAL>();
            buider.RegisterType<ThingsDAL>().As<IThingsDAL>();
            buider.RegisterType<LendsDAL>().As<ILendsDAL>();
            buider.RegisterType<HistoryDAL>().As<IHistoryDAL>();
            buider.RegisterType<Storage>().AsSelf();
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