using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Tests
{
    [TestFixture]
    public class ThingsBLTests
    {
        private IThingsBL _thingsBL;
        private Mock<IUsersDAL> _users;
        private Mock<IFriendsDAL> _friends;
        private Mock<ICategoriesDAL> _categories;
        private Mock<IHistoryDAL> _history;
        private Mock<IThingsDAL> _things;
        private Mock<ILendsDAL> _lends;

        [SetUp]
        public void Setup()
        {
            _friends = new Mock<IFriendsDAL>();
            _friends.Setup(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userF, Guid idF) => Task.FromResult(new Friend { Id = idF, Name = "Mock" }));
            _users = new Mock<IUsersDAL>();
            _history = new Mock<IHistoryDAL>();
            _history.Setup(h => h.DeleteThingHistory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _history.Setup(h => h.GetThingHistLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Dictionary<HistoricalLend, Friend>() as IDictionary<HistoricalLend, Friend>));
            _categories = new Mock<ICategoriesDAL>();
            _categories.Setup(c => c.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userC, Guid idC) => Task.FromResult(new Category { Id = idC, Name = "Mock" }));
            _categories.Setup(c => c.GetCategories(It.IsAny<Guid>()))
                .Returns((Guid userC) => Task.FromResult(new List<Category>() as IEnumerable<Category>));
            _categories.Setup(c => c.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns(Task.CompletedTask);
            _categories.Setup(c => c.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns(Task.CompletedTask);
            _categories.Setup(c => c.DeleteCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things = new Mock<IThingsDAL>();
            _things.Setup(t => t.CreateThing(It.IsAny<Guid>(), It.IsAny<Thing>()))
                .Returns(Task.CompletedTask);
            _things.Setup(t => t.UpdateThing(It.IsAny<Guid>(), It.IsAny<Thing>()))
                .Returns(Task.CompletedTask);
            _things.Setup(t => t.UpdateThingsCategory(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
               .Returns(Task.CompletedTask);
            _things.Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userT, Guid idT) => Task.FromResult(new Thing { Id = idT, Name = "Mock" }));
            _things.Setup(t => t.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userT, Guid idT) => Task.FromResult(new List<Thing>() as IEnumerable<Thing>));
            _things.Setup(t => t.GetThings(It.IsAny<Guid>()))
                .Returns((Guid userT) => Task.FromResult(new List<Thing>() as IEnumerable<Thing>));
            _things.Setup(t => t.DeleteThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .Returns(Task.CompletedTask);
            _things.Setup(t => t.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .Returns(Task.CompletedTask);
            _lends = new Mock<ILendsDAL>();
            var dal = new CommonDAL(_users.Object, _friends.Object, _categories.Object, _things.Object, _lends.Object, _history.Object);
            _thingsBL = new ThingsBL(dal);
        }

        [Test]
        public async Task CreateThingTest()
        {
            var thing = new Models.ThingWithLend { };
            var res = await _thingsBL.CreateThing(new Guid(), thing);
            Assert.NotNull(res);
            Assert.AreEqual(thing.Id, res.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _thingsBL.CreateThing(new Guid(), null);
            });
            _things.Verify(t => t.CreateThing(It.IsAny<Guid>(), It.IsAny<Thing>()), Times.Once());
        }

        [Test]
        public async Task UpdateThingTest()
        {
            var thing = new Models.ThingWithLend { };
            var res = await _thingsBL.UpdateThing(new Guid(), thing);
            Assert.NotNull(res);
            Assert.AreEqual(thing.Id, res.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _thingsBL.UpdateThing(new Guid(), null);
            });
            _things.Verify(t => t.UpdateThing(It.IsAny<Guid>(), It.IsAny<Thing>()), Times.Once());
        }

        [Test]
        public async Task DeleteThingTest()
        {
            await _thingsBL.DeleteThing(new Guid(), new Guid());
            _things.Verify(t => t.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _history.Verify(t => t.DeleteThingHistory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetThingTest()
        {
            var id = SequentialGuidUtils.CreateGuid();
            var res = await _thingsBL.GetThing(new Guid(), id);
            Assert.NotNull(res);
            Assert.AreEqual(id, res.Id);
            _things.Verify(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetThingsTest()
        {
            var res = await _thingsBL.GetThings(new Guid());
            Assert.NotNull(res);
            _things.Verify(t => t.GetThings(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetThingsForCategoryTest()
        {
            var res = await _thingsBL.GetThingsForCategory(new Guid(), new Guid());
            Assert.NotNull(res);
            _things.Verify(t => t.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetThingLendsTest()
        {
            var res = await _thingsBL.GetThingLends(new Guid(), new Guid());
            Assert.NotNull(res);
            _things.Verify(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _history.Verify(t => t.GetThingHistLends(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task CreateCategoryTest()
        {
            var category = new Models.Category { };
            var res = await _thingsBL.CreateCategory(new Guid(), category);
            Assert.NotNull(res);
            Assert.AreEqual(category.Id, res.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _thingsBL.CreateCategory(new Guid(), null);
            });
            _categories.Verify(t => t.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()), Times.Once());
        }

        [Test]
        public async Task UpdateCategoryTest()
        {
            var category = new Models.Category { };
            var res = await _thingsBL.UpdateCategory(new Guid(), category);
            Assert.NotNull(res);
            Assert.AreEqual(category.Id, res.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _thingsBL.UpdateCategory(new Guid(), null);
            });
            _categories.Verify(t => t.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()), Times.Once());
        }

        [Test]
        public async Task DeleteCategoryWithThingsTest()
        {
            await _thingsBL.DeleteCategoryWithThings(new Guid(), new Guid());
            _categories.Verify(t => t.DeleteCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _things.Verify(t => t.DeleteThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task DeleteCategoryWithReplacementTest()
        {
            await _thingsBL.DeleteCategoryWithReplacement(new Guid(), new Guid(), new Guid());
            _categories.Verify(t => t.DeleteCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _things.Verify(t => t.UpdateThingsCategory(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetCategoryTest()
        {
            var id = SequentialGuidUtils.CreateGuid();
            var res = await _thingsBL.GetCategory(new Guid(), id);
            Assert.NotNull(res);
            Assert.AreEqual(id, res.Id);
            _categories.Verify(t => t.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetCategoriesTest()
        {
            var res = await _thingsBL.GetCategories(new Guid());
            Assert.NotNull(res);
            _categories.Verify(t => t.GetCategories(It.IsAny<Guid>()), Times.Once());
        }
    }
}
