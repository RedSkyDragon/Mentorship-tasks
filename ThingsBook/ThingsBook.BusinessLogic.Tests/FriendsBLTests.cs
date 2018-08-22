using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Tests
{
    [TestFixture]
    public class FriendsBLTests
    {
        private IFriendsBL _friendsBL;
        private Mock<IUsers> _users;
        private Mock<IFriends> _friends;
        private Mock<ICategories> _categories;
        private Mock<IHistory> _history;
        private Mock<IThings> _things;
        private Mock<ILends> _lends;

        [SetUp]
        public void Setup()
        {
            _friends = new Mock<IFriends>();
            _friends.Setup(f => f.DeleteFriends(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _friends.Setup(f => f.CreateFriend(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns(Task.CompletedTask);
            _friends.Setup(f => f.UpdateFriend(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns(Task.CompletedTask);
            _friends.Setup(f => f.DeleteFriend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _friends.Setup(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid user, Guid id) => Task.FromResult(new Friend{ Id = id, Name = "Mock" }));
            _friends.Setup(f => f.GetFriends(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new List<Friend>() as IEnumerable<Friend>));
            _users = new Mock<IUsers>();
            _history = new Mock<IHistory>();
            _history.Setup(h => h.DeleteFriendHistory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _history.Setup(h => h.GetFriendHistLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Dictionary<HistoricalLend, Thing>() as IDictionary<HistoricalLend, Thing>));
            _categories = new Mock<ICategories>();
            _things = new Mock<IThings>();
            _things.Setup(t => t.GetThingsForFriend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Thing>() as IEnumerable<Thing>));
            _lends = new Mock<ILends>();
            _lends.Setup(l => l.DeleteFriendLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            var dal = new CommonDAL(_users.Object, _friends.Object, _categories.Object, _things.Object, _lends.Object, _history.Object);
            _friendsBL = new FriendsBL(dal);
        }

        [Test]
        public async Task CreateFriendTest()
        {
            var friend = new Models.Friend();
            var result = await _friendsBL.Create(new Guid(), friend);
            Assert.NotNull(result);
            Assert.AreEqual(friend.Id, result.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _friendsBL.Create(new Guid(), null);
            });
            _friends.Verify(f => f.CreateFriend(It.IsAny<Guid>(), It.IsAny<Friend>()), Times.Once());
        }

        [Test]
        public async Task UpdateFriendTest()
        {
            var friend = new Models.Friend();
            var result = await _friendsBL.Update(new Guid(), friend);
            Assert.NotNull(result);
            Assert.AreEqual(friend.Id, result.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _friendsBL.Update(new Guid(), null);
            });
            _friends.Verify(f => f.UpdateFriend(It.IsAny<Guid>(), It.IsAny<Friend>()), Times.Once());
        }

        [Test]
        public async Task GetFriendTest()
        {
            var friend = new Models.Friend();
            var result = await _friendsBL.GetOne(new Guid(), friend.Id);
            Assert.NotNull(result);
            Assert.AreEqual(friend.Id, result.Id);
            _friends.Verify(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetAllFriendTest()
        {
            var result = await _friendsBL.GetAll(new Guid());
            Assert.NotNull(result);
            _friends.Verify(f => f.GetFriends(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task DeleteFriendTest()
        {
            await _friendsBL.Delete(new Guid(), new Guid());
            _friends.Verify(f => f.DeleteFriend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetFriendLendsTest()
        {
            var result = await _friendsBL.GetFriendLends(new Guid(), new Guid());
            Assert.NotNull(result);
            _friends.Verify(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }
    }
}
