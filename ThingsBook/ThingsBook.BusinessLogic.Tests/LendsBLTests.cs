using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Tests
{
    [TestFixture]
    public class LendsBLTests
    {
        private ILendsBL _lendsBL;
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
            _history.Setup(h => h.CreateHistLend(It.IsAny<Guid>(), It.IsAny<HistoricalLend>()))
                .Returns(Task.CompletedTask);
            _history.Setup(h => h.DeleteHistLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _history.Setup(h => h.GetHistLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userId, Guid id) => 
                    Task.FromResult(new HistoricalLend { Id = id, FriendId = new Guid(), ThingId = new Guid() })
                );
            _history.Setup(h => h.GetHistLends(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<HistoricalLend>() as IEnumerable<HistoricalLend>));
            _categories = new Mock<ICategoriesDAL>();
            _things = new Mock<IThingsDAL>();
            _things.Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid userT, Guid idT) => Task.FromResult(new Thing { Id = idT, Name = "Mock", Lend = new Lend { } }));
            _lends = new Mock<ILendsDAL>();
            _lends.Setup(l => l.CreateLend(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns(Task.CompletedTask);
            _lends.Setup(l => l.UpdateLend(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns(Task.CompletedTask);
            _lends.Setup(l => l.DeleteLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            var dal = new CommonDAL(_users.Object, _friends.Object, _categories.Object, _things.Object, _lends.Object, _history.Object);
            _lendsBL = new LendsBL(dal);
        }

        [Test]
        public async Task CreateTest()
        {
            var lend = new Models.Lend { };
            var thingId = SequentialGuidUtils.CreateGuid();
            var res = await _lendsBL.Create(new Guid(), thingId, lend);
            Assert.NotNull(res);
            Assert.AreEqual(thingId, res.Id);
            _lends.Verify(t => t.CreateLend(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()), Times.Once());
        }

        [Test]
        public async Task UpdateTest()
        {
            var lend = new Models.Lend { };
            var thingId = SequentialGuidUtils.CreateGuid();
            var res = await _lendsBL.Update(new Guid(), thingId, lend);
            Assert.NotNull(res);
            Assert.AreEqual(thingId, res.Id);
            _lends.Verify(t => t.UpdateLend(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()), Times.Once());
        }

        [Test]
        public async Task DeleteTest()
        {
            var thingId = SequentialGuidUtils.CreateGuid();
            await _lendsBL.Delete(new Guid(), thingId, DateTime.Now);
            _lends.Verify(t => t.DeleteLend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _things.Verify(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _history.Verify(t => t.CreateHistLend(It.IsAny<Guid>(), It.IsAny<HistoricalLend>()), Times.Once());
        }

        [Test]
        public async Task DeleteHistoricalLendTest()
        {
            await _lendsBL.DeleteHistoricalLend(new Guid(), new Guid());
            _history.Verify(t => t.DeleteHistLend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetHistoricalLendTest()
        {
            var id = SequentialGuidUtils.CreateGuid();
            var res = await _lendsBL.GetHistoricalLend(new Guid(), id);
            Assert.NotNull(res);
            Assert.AreEqual(id, res.Id);
            _history.Verify(t => t.GetHistLend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _things.Verify(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
            _friends.Verify(t => t.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetHistoricalLendsTest()
        {
            var res = await _lendsBL.GetHistoricalLends(new Guid());
            Assert.NotNull(res);
            _history.Verify(t => t.GetHistLends(It.IsAny<Guid>()), Times.Once());
        }
    }
}
