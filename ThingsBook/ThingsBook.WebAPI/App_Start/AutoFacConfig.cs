using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MongoDB.Driver;
using ThingsBook.BusinessLogic;
using ThingsBook.Data.Interface;
using ThingsBook.Data.Mongo;

namespace ThingsBook.WebAPI
{
    /// <summary>
    /// AutoFac configuration.
    /// </summary>
    public class AutoFacConfig
    {
        /// <summary>
        /// configures AutoFac options for current project
        /// </summary>
        public void ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());           
            RegisterDAL(builder);
            RegisterBL(builder);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Registers the DAL injections.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected virtual void RegisterDAL(ContainerBuilder builder)
        {
            builder.RegisterType<MongoClient>().As<IMongoClient>();
            builder.RegisterType<ThingsBookContext>().AsSelf()
                .WithParameter("connectionString", ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString)
                .SingleInstance();
            builder.RegisterType<UsersDAL>().As<IUsersDAL>();
            builder.RegisterType<FriendsDAL>().As<IFriendsDAL>();
            builder.RegisterType<CategoriesDAL>().As<ICategoriesDAL>();
            builder.RegisterType<ThingsDAL>().As<IThingsDAL>();
            builder.RegisterType<LendsDAL>().As<ILendsDAL>();
            builder.RegisterType<HistoryDAL>().As<IHistoryDAL>();
            builder.RegisterType<Storage>().AsSelf();
        }

        /// <summary>
        /// Registers the BL injections.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected virtual void RegisterBL(ContainerBuilder builder)
        {
            builder.RegisterType<UsersBL>().As<IUsersBL>();
            builder.RegisterType<FriendsBL>().As<IFriendsBL>();
            builder.RegisterType<ThingsBL>().As<IThingsBL>();
            builder.RegisterType<LendsBL>().As<ILendsBL>();
        }
    }
}