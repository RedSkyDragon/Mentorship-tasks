using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Tests
{
    [TestFixture]
    public class LendsBLTests
    {
        private ILendsBL _lendsBL;

        [SetUp]
        public void Setup()
        {
            var friends = new Mock<IFriends>();
            friends.Setup(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userF, Guid idF) => Task.FromResult(new Friend { Id = idF, Name = "Mock" }));
            var users = new Mock<IUsers>();
            var history = new Mock<IHistory>();
            history.Setup(h => h.CreateHistLend(It.IsAny<Guid>(), It.Is<HistoricalLend>(l => l != null)))
                .Returns(Task.Delay(1));
            history.Setup(h => h.DeleteHistLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.Delay(1));
            history.Setup(h => h.GetHistLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userId, Guid id) => 
                    Task.FromResult(new HistoricalLend { Id = id, FriendId = new Guid(), ThingId = new Guid() })
                );
            history.Setup(h => h.GetHistLends(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<HistoricalLend>() as IEnumerable<HistoricalLend>));
            var categories = new Mock<ICategories>();
            var things = new Mock<IThings>();
            things.Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userT, Guid idT) => Task.FromResult(new Thing { Id = idT, Name = "Mock", Lend = new Lend { } }));
            var lends = new Mock<ILends>();
            lends.Setup(l => l.CreateLend(It.IsAny<Guid>(), It.IsAny<Guid>(), It.Is<Lend>(lend => lend != null)))
                .Returns(Task.Delay(1));
            lends.Setup(l => l.UpdateLend(It.IsAny<Guid>(), It.IsAny<Guid>(), It.Is<Lend>(lend => lend != null)))
                .Returns(Task.Delay(1));
            lends.Setup(l => l.DeleteLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.Delay(1));
            var dal = new CommonDAL(users.Object, friends.Object, categories.Object, things.Object, lends.Object, history.Object);
            _lendsBL = new LendsBL(dal);
        }

        [Test]
        public void CreateTest()
        {
            var lend = new Models.Lend { };
            var thingId = SequentialGuidUtils.CreateGuid();
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _lendsBL.Create(new Guid(), thingId, lend);
                Assert.NotNull(res);
                Assert.AreEqual(thingId, res.Id);
            });
        }

        [Test]
        public void UpdateTest()
        {
            var lend = new Models.Lend { };
            var thingId = SequentialGuidUtils.CreateGuid();
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _lendsBL.Update(new Guid(), thingId, lend);
                Assert.NotNull(res);
                Assert.AreEqual(thingId, res.Id);
            });
        }

        [Test]
        public void DeleteTest()
        {
            var thingId = SequentialGuidUtils.CreateGuid();
            Assert.DoesNotThrowAsync(() =>
            {
                return _lendsBL.Delete(new Guid(), thingId, DateTime.Now);
            });
        }

        [Test]
        public void DeleteHistoricalLendTest()
        {
            Assert.DoesNotThrowAsync(() =>
            {
                return _lendsBL.DeleteHistoricalLend(new Guid(), new Guid());
            });
        }

        [Test]
        public void GetHistoricalLendTest()
        {
            var id = SequentialGuidUtils.CreateGuid();
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _lendsBL.GetHistoricalLend(new Guid(), id);
                Assert.NotNull(res);
                Assert.AreEqual(id, res.Id);
            });
        }

        [Test]
        public void GetHistoricalLendsTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var res = await _lendsBL.GetHistoricalLends(new Guid());
                Assert.NotNull(res);
            });
        }
    }
}
