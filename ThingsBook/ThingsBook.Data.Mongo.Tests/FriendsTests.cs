﻿using System.Configuration;
using MongoDB.Driver;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class FriendsTests
    {
        private IFriendsDAL _friends;
        private IUsersDAL _users;
        private User _user;
        private Friend _friend;
        private const string Sample = "Sample";

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString, new MongoClient());
            _users = new UsersDAL(context);
            _user = new User { Name = Sample };
            _friends = new FriendsDAL(context);
            _friend = new Friend { Name = Sample, Contacts = Sample, UserId = _user.Id };
            await _users.CreateUser(_user);
            await _friends.CreateFriend(_user.Id, _friend);
        }

        [Test]
        [Explicit]
        public async Task CreateFriendTest()
        {
            var friend = new Friend { Name = Sample, Contacts = Sample, UserId = _user.Id };
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
            Assert.NotNull(first);
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
            Assert.True(friends.Any());
        }

        [Test]
        [Explicit]
        public async Task UpdateFriendTest()
        {
            _friend.Name = "Updated";
            _friend.Contacts = "Updated";
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
            var friend = new Friend { Name = Sample, Contacts = Sample, UserId = _user.Id };
            await _friends.CreateFriend(_user.Id, friend);
            await _friends.DeleteFriend(_user.Id, friend.Id);
            var dbFriend = await _friends.GetFriend(_user.Id, friend.Id);
            Assert.Null(dbFriend);
        }

        [Test]
        [Explicit]
        public async Task DeleteFriendsTest()
        {
            var user = new User { Name = Sample };
            await _users.CreateUser(user);
            var friend1 = new Friend { Name = Sample, Contacts = Sample, UserId = user.Id };
            var friend2 = new Friend { Name = Sample, Contacts = Sample, UserId = user.Id };
            await _friends.CreateFriend(user.Id, friend1);
            await _friends.CreateFriend(user.Id, friend2);
            await _friends.DeleteFriends(user.Id);
            var friends = (await _friends.GetFriends(user.Id)).ToList();
            Assert.True(!friends.Any());
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
