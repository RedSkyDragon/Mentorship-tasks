using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class FriendsTests
    {
        private IFriends _friends;
        private IUsers _users;
        private User _user;
        private Friend _friend;

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext("mongodb://localhost/ThingsBookTests");
            _users = new Users(context);
            _user = new User { Name = "FriendTest User" };
            _friends = new Friends(context);
            _friend = new Friend { Name = "Test Friend", Contacts = "Test contacts", UserId = _user.Id };
            await _users.CreateUser(_user);
            await _friends.CreateFriend(_user.Id, _friend);
        }

        [Test]
        [Explicit]
        public async Task CreateFriendTest()
        {
            var friend = new Friend { Name = "Test Friend Create", Contacts = "Test contacts", UserId = _user.Id };
            await _friends.CreateFriend(_user.Id, friend);
            var dbFriend = await _friends.GetFriend(_user.Id, friend.Id);
            Assert.AreEqual(friend.Id, dbFriend.Id);
            Assert.AreEqual(friend.Name, dbFriend.Name);
            Assert.AreEqual(friend.Contacts, dbFriend.Contacts);
            Assert.AreEqual(friend.UserId, dbFriend.UserId);
            await _friends.DeleteFriend(_user.Id, friend.Id);
        }

        [Test]
        [Explicit]
        public async Task GetFriendTest()
        {
            var first = await _friends.GetFriend(_user.Id, _friend.Id);
            var second = await _friends.GetFriend(_user.Id, _friend.Id);
            Assert.AreNotEqual(null, first);
            Assert.AreEqual(first.Id, second.Id);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.Contacts, second.Contacts);
            Assert.AreEqual(first.UserId, second.UserId);
        }

        [Test]
        [Explicit]
        public async Task GetAllFriendsTest()
        {
            var friends = await _friends.GetFriends(_user.Id);
            Assert.AreEqual(true, friends.Count() > 0);
        }

        [Test]
        [Explicit]
        public async Task UpdateFriendTest()
        {
            _friend.Name = "Updated name";
            _friend.Contacts = "Updated contacts";
            await _friends.UpdateFriend(_user.Id, _friend);
            var updated = await _friends.GetFriend(_user.Id, _friend.Id);
            Assert.AreEqual(_friend.Id, updated.Id);
            Assert.AreEqual(_friend.Name, updated.Name);
            Assert.AreEqual(_friend.Contacts, updated.Contacts);
            Assert.AreEqual(_friend.UserId, updated.UserId);
        }

        [Test]
        [Explicit]
        public async Task DeleteFriendTest()
        {
            var friend = new Friend { Name = "Test Friend Delete", Contacts = "Test contacts", UserId = _user.Id };
            await _friends.CreateFriend(_user.Id, friend);
            await _friends.DeleteFriend(_user.Id, friend.Id);
            var dbFriend = await _friends.GetFriend(_user.Id, friend.Id);
            Assert.AreEqual(null, dbFriend);
        }

        [Test]
        [Explicit]
        public async Task DeleteFriendsTest()
        {
            var user = new User { Name = "Delete Test" };
            await _users.CreateUser(user);
            var friend1 = new Friend { Name = "Test Friend Delete", Contacts = "Test contacts", UserId = user.Id };
            var friend2 = new Friend { Name = "Test Friend Delete", Contacts = "Test contacts", UserId = user.Id };
            await _friends.CreateFriend(user.Id, friend1);
            await _friends.CreateFriend(user.Id, friend2);
            await _friends.DeleteFriends(user.Id);
            var friends = (await _friends.GetFriends(user.Id)).ToList();
            Assert.AreEqual(true, friends.Count() == 0);
            await _users.DeleteUser(user.Id);
        }

        [TearDown]
        public async Task Final()
        {
            await _users.DeleteUser(_user.Id);
            await _friends.DeleteFriend(_user.Id, _friend.Id);
        }
    }
}
