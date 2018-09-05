using Autofac;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;

namespace ThingsBook.WebAPI.Tests.Utils
{
    /// <summary>
    /// Test AutoFac configuration
    /// </summary>
    public class TestAutoFac: AutoFacConfig 
    {
        /// <summary>
        /// Registers the BL injections.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void RegisterBL(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetUsersMock().Object).As<IUsersBL>();
            builder.RegisterInstance(GetFriendsMock().Object).As<IFriendsBL>();
            builder.RegisterInstance(GetThingsMock().Object).As<IThingsBL>();
            builder.RegisterInstance(GetLendsMock().Object).As<ILendsBL>();
        }

        /// <summary>
        /// Registers the DAL injections.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void RegisterDAL(ContainerBuilder builder) { }

        private static Mock<IUsersBL> GetUsersMock()
        {
            var users = new Mock<IUsersBL>();
            users
                .Setup(u => u.Get(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new User { Id = id, Name = "UserName" }));
            users
                .Setup(u => u.CreateOrUpdate(It.IsAny<User>()))
                .Returns((User user) => Task.FromResult(user));
            users
                .Setup(u => u.Delete(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            return users;
        }

        private static Mock<IThingsBL> GetThingsMock()
        {
            var things = new Mock<IThingsBL>();
            things
                .Setup(t => t.CreateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()))
                .Returns((Guid id, ThingWithLend th) => Task.FromResult(th));
            things
                .Setup(t => t.UpdateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()))
                .Returns((Guid id, ThingWithLend th) => Task.FromResult(th));
            things
                .Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid th) => Task.FromResult(new ThingWithLend { Id = th, Name = "Sample", About = "Sample" }));
            things
                .Setup(t => t.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            things
                .Setup(t => t.GetThingLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new FilteredLends()));
            things
                .Setup(t => t.GetThings(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ThingWithLend>() as IEnumerable<ThingWithLend>));
            things
                .Setup(t => t.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns((Guid id, Category cat) => Task.FromResult(cat));
            things
                .Setup(t => t.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns((Guid id, Category cat) => Task.FromResult(cat));
            things
                .Setup(t => t.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid cat) => Task.FromResult(new Category { Id = cat, Name = "Sample", About = "Sample" }));
            things
                .Setup(t => t.DeleteCategoryWithThings(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            things
                .Setup(t => t.DeleteCategoryWithReplacement(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            things
                .Setup(t => t.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ThingWithLend>() as IEnumerable<ThingWithLend>));
            things
                .Setup(t => t.GetCategories(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Category>() as IEnumerable<Category>));
            return things;
        }

        private static Mock<IFriendsBL> GetFriendsMock()
        {
            var friends = new Mock<IFriendsBL>();
            friends
                .Setup(t => t.Create(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns((Guid id, Friend fr) => Task.FromResult(fr));
            friends
                .Setup(t => t.Update(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns((Guid id, Friend fr) => Task.FromResult(fr));
            friends
                .Setup(t => t.GetOne(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid fr) => Task.FromResult(new Friend { Id = fr, Name = "Sample", Contacts = "Sample" }));
            friends
                .Setup(t => t.Delete(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            friends
                .Setup(t => t.GetFriendLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new FilteredLends()));
            friends
                .Setup(t => t.GetAll(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Friend>() as IEnumerable<Friend>));
            return friends;
        }

        private static Mock<ILendsBL> GetLendsMock()
        {
            var lends = new Mock<ILendsBL>();
            lends
                .Setup(t => t.GetHistoricalLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid hid) => Task.FromResult(new HistLend { Id = hid }));
            lends
                .Setup(t => t.GetHistoricalLends(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new List<HistLend>() as IEnumerable<HistLend>));
            lends
                .Setup(t => t.DeleteHistoricalLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            lends
                .Setup(t => t.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns((Guid id, Guid th, Lend lend) => Task.FromResult(new ThingWithLend { Id = th, Lend = lend }));
            lends
                .Setup(t => t.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns((Guid id, Guid th, Lend lend) => Task.FromResult(new ThingWithLend { Id = th, Lend = lend }));
            lends
                .Setup(t => t.Delete(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);
            return lends;
        }
    }
}
