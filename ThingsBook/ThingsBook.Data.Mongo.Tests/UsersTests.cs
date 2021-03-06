﻿using System.Configuration;
using MongoDB.Driver;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class UsersTests
    {
        private IUsersDAL _users;
        private User _user;
        private const string Sample = "Sample";

        [SetUp]
        [Explicit]
        public async Task Setup()
        {
            _users = new UsersDAL(new ThingsBookContext(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString, new MongoClient()));
            _user = new User { Name = Sample };
            await _users.CreateUser(_user);
        }

        [Test]
        [Explicit]
        public async Task CreateUserTest()
        {
            var user = new User { Name = Sample };
            await _users.CreateUser(user);
            var dbUser = await _users.GetUser(user.Id);
            Assert.AreEqual(user.Id, dbUser.Id);
            Assert.AreEqual(user.Name, dbUser.Name);
            await _users.DeleteUser(user.Id);
        }

        [Test]
        [Explicit]
        public async Task UpdateUserTest()
        {
            _user.Name = "Updated name";
            await _users.UpdateUser(_user);
            var updated = await _users.GetUser(_user.Id);
            Assert.AreEqual(updated.Id, _user.Id);
            Assert.AreEqual(updated.Name, _user.Name);
        }

        [Test]
        [Explicit]
        public async Task GetUserTest()
        {
            var firstUser = await _users.GetUser(_user.Id);
            var secondUser = await _users.GetUser(_user.Id);
            Assert.AreEqual(firstUser.Id, secondUser.Id);
            Assert.AreEqual(firstUser.Name, secondUser.Name);
        }

        [Test]
        [Explicit]
        public async Task GetAllUsersTest()
        {
            var user1 = new User { Name = Sample + "1" };
            await _users.CreateUser(user1);
            var user2 = new User { Name = Sample + "1" };
            await _users.CreateUser(user2);
            var users = (await _users.GetUsers()).ToList();
            Assert.NotNull(users);
            Assert.AreEqual(user1.Name, users.Find(u => u.Id == user1.Id).Name);
            Assert.AreEqual(user2.Name, users.Find(u => u.Id == user2.Id).Name);
            foreach (var user in users)
            {
                await _users.DeleteUser(user.Id);
            }           
        }

        [Test]
        [Explicit]
        public async Task DeleteUserTest()
        {
            var user = new User { Name = Sample };
            await _users.CreateUser(user);
            await _users.DeleteUser(user.Id);
            var res = await _users.GetUser(user.Id);
            Assert.Null(res);
        }

        [TearDown]
        [Explicit]
        public async Task Final()
        {
            await _users.DeleteUser(_user.Id);
        }
    }
}
