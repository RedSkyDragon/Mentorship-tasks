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

        [SetUp]
        public void Setup()
        {
            var friends = new Mock<IFriends>();
            friends.Setup(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userF, Guid idF) => Task.FromResult(new Friend { Id = idF, Name = "Mock" }));
            var users = new Mock<IUsers>();
            var history = new Mock<IHistory>();
            history.Setup(h => h.DeleteThingHistory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.Delay(1));
            history.Setup(h => h.GetThingHistLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Dictionary<HistoricalLend, Friend>() as IDictionary<HistoricalLend, Friend>));
            var categories = new Mock<ICategories>();
            categories.Setup(c => c.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userC, Guid idC) => Task.FromResult(new Category { Id = idC, Name = "Mock" }));
            categories.Setup(c => c.GetCategories(It.IsAny<Guid>()))
                .Returns((Guid userC) => Task.FromResult(new List<Category>() as IEnumerable<Category>));
            categories.Setup(c => c.CreateCategory(It.IsAny<Guid>(), It.Is<Category>(cat => cat != null)))
                .Returns(Task.Delay(1));
            categories.Setup(c => c.UpdateCategory(It.IsAny<Guid>(), It.Is<Category>(cat => cat != null)))
                .Returns(Task.Delay(1));
            categories.Setup(c => c.DeleteCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.Delay(1));
            var things = new Mock<IThings>();
            things.Setup(t => t.CreateThing(It.IsAny<Guid>(), It.Is<Thing>(arg => arg != null)))
                .Returns(Task.Delay(1));
            things.Setup(t => t.UpdateThing(It.IsAny<Guid>(), It.Is<Thing>(arg => arg != null)))
                .Returns(Task.Delay(1));
            things.Setup(t => t.UpdateThingsCategory(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
               .Returns(Task.Delay(1));
            things.Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userT, Guid idT) => Task.FromResult(new Thing { Id = idT, Name = "Mock" }));
            things.Setup(t => t.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userT, Guid idT) => Task.FromResult(new List<Thing>() as IEnumerable<Thing>));
            things.Setup(t => t.GetThings(It.IsAny<Guid>()))
                .Returns((Guid userT) => Task.FromResult(new List<Thing>() as IEnumerable<Thing>));
            things.Setup(t => t.DeleteThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .Returns(Task.Delay(1));
            things.Setup(t => t.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .Returns(Task.Delay(1));
            var lends = new Mock<ILends>();
            var dal = new CommonDAL(users.Object, friends.Object, categories.Object, things.Object, lends.Object, history.Object);
            _thingsBL = new ThingsBL(dal);
        }

        [Test]
        public void CreateThingTest()
        {
            var thing = new Models.ThingWithLend { };
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.CreateThing(new Guid(), thing);
                Assert.NotNull(res);
                Assert.AreEqual(thing.Id, res.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _thingsBL.CreateThing(new Guid(), null);
            });
        }

        [Test]
        public void UpdateThingTest()
        {
            var thing = new Models.ThingWithLend { };
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.UpdateThing(new Guid(), thing);
                Assert.NotNull(res);
                Assert.AreEqual(thing.Id, res.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _thingsBL.UpdateThing(new Guid(), null);
            });
        }

        [Test]
        public void DeleteThingTest()
        {
            Assert.DoesNotThrowAsync(() =>
            {
                return _thingsBL.DeleteThing(new Guid(), new Guid());
            });
        }

        [Test]
        public void GetThingTest()
        {
            var id = SequentialGuidUtils.CreateGuid();
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.GetThing(new Guid(), id);
                Assert.NotNull(res);
                Assert.AreEqual(id, res.Id);
            });
        }

        [Test]
        public void GetThingsTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.GetThings(new Guid());
                Assert.NotNull(res);
            });
        }

        [Test]
        public void GetThingsForCategoryTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.GetThingsForCategory(new Guid(), new Guid());
                Assert.NotNull(res);
            });
        }

        [Test]
        public void GetThingLendsTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.GetThingLends(new Guid(), new Guid());
                Assert.NotNull(res);
            });
        }

        [Test]
        public void CreateCategoryTest()
        {
            var category = new Models.Category { };
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.CreateCategory(new Guid(), category);
                Assert.NotNull(res);
                Assert.AreEqual(category.Id, res.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _thingsBL.CreateCategory(new Guid(), null);
            });
        }

        [Test]
        public void UpdateCategoryTest()
        {
            var category = new Models.Category { };
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.UpdateCategory(new Guid(), category);
                Assert.NotNull(res);
                Assert.AreEqual(category.Id, res.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _thingsBL.UpdateCategory(new Guid(), null);
            });
        }

        [Test]
        public void DeleteCategoryWithThingsTest()
        {
            Assert.DoesNotThrowAsync(() =>
            {
                return _thingsBL.DeleteCategoryWithThings(new Guid(), new Guid());
            });
        }

        [Test]
        public void DeleteCategoryWithReplacementTest()
        {
            Assert.DoesNotThrowAsync(() =>
            {
                return _thingsBL.DeleteCategoryWithReplacement(new Guid(), new Guid(), new Guid());
            });
        }

        [Test]
        public void GetCategoryTest()
        {
            var id = SequentialGuidUtils.CreateGuid();
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.GetCategory(new Guid(), id);
                Assert.NotNull(res);
                Assert.AreEqual(id, res.Id);
            });
        }

        [Test]
        public void GetCategoriesTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _thingsBL.GetCategories(new Guid());
                Assert.NotNull(res);
            });
        }
    }
}
