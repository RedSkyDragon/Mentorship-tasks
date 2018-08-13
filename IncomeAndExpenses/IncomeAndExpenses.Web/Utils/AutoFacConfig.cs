using Autofac;
using Autofac.Integration.Mvc;
using IncomeAndExpenses.DataAccessImplement;
using IncomeAndExpenses.DataAccessInterface;
using IncomeAndExpenses.BusinessLogic;
using System.Data.Entity;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
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
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(c => new InAndExDbContext()).As<DbContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<IncomesBL>().As<IIncomesBL>().InstancePerRequest();
            builder.RegisterType<ExpensesBL>().As<IExpensesBL>().InstancePerRequest();
            builder.RegisterType<UsersBL>().As<IUsersBL>().InstancePerRequest();
            builder.RegisterType<TotalsBL>().As<ITotalsBL>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}