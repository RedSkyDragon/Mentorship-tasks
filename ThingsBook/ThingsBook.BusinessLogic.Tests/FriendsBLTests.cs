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

        [SetUp]
        public void Setup()
        {
            var friends = new Mock<IFriends>();
            friends.Setup(f => f.DeleteFriends(It.IsAny<Guid>())).Returns(Task.Delay(1));
            friends.Setup(f => f.CreateFriend(It.IsAny<Guid>(), It.Is<Friend>(fr => fr != null))).Returns(Task.Delay(1));
            friends.Setup(f => f.UpdateFriend(It.IsAny<Guid>(), It.Is<Friend>(fr => fr != null))).Returns(Task.Delay(1));
            friends.Setup(f => f.DeleteFriend(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.Delay(1));
            friends.Setup(f => f.GetFriend(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((Guid user, Guid id) => Task.FromResult(new Friend{ Id = id, Name = "Mock" }));
            friends.Setup(f => f.GetFriends(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(new List<Friend>() as IEnumerable<Friend>));
            var users = new Mock<IUsers>();
            var history = new Mock<IHistory>();
            history.Setup(h => h.DeleteFriendHistory(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.Delay(1));
            history.Setup(h => h.GetFriendHistLends(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(new Dictionary<HistoricalLend, Thing>() as IDictionary<HistoricalLend, Thing>));
            var categories = new Mock<ICategories>();
            var things = new Mock<IThings>();
            things.Setup(t => t.GetThingsForFriend(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(new List<Thing>() as IEnumerable<Thing>));
            var lends = new Mock<ILends>();
            lends.Setup(l => l.DeleteFriendLends(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.Delay(1));
            var dal = new CommonDAL(users.Object, friends.Object, categories.Object, things.Object, lends.Object, history.Object);
            _friendsBL = new FriendsBL(dal);
        }

        [Test]
        public void CreateFriendTest()
        {
            var friend = new Models.Friend();
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await _friendsBL.Create(new Guid(), friend);
                Assert.NotNull(result);
                Assert.AreEqual(friend.Id, result.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _friendsBL.Create(new Guid(), null);
            });
        }

        [Test]
        public void UpdateFriendTest()
        {
            var friend = new Models.Friend();
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await _friendsBL.Update(new Guid(), friend);
                Assert.NotNull(result);
                Assert.AreEqual(friend.Id, result.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _friendsBL.Update(new Guid(), null);
            });
        }

        [Test]
        public void GetFriendTest()
        {
            var friend = new Models.Friend();
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await _friendsBL.GetOne(new Guid(), friend.Id);
                Assert.NotNull(result);
                Assert.AreEqual(friend.Id, result.Id);
            });
        }

        [Test]
        public void GetAllFriendTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await _friendsBL.GetAll(new Guid());
                Assert.NotNull(result);
            });
        }

        [Test]
        public void DeleteFriendTest()
        {
            Assert.DoesNotThrowAsync(() =>
            {
                return _friendsBL.Delete(new Guid(), new Guid());
            });
        }

        [Test]
        public void GetFriendLendsTest()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await _friendsBL.GetFriendLends(new Guid(), new Guid());
                Assert.NotNull(result);
            });
        }
    }
}
