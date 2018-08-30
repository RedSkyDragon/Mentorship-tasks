﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic.Tests
{
    [TestFixture]
    public class UsersBLTests
    {
        private IUsersBL _usersBL;
        private Mock<IUsers> _users;
        private Mock<IFriends> _friends;
        private Mock<ICategories> _categories;
        private Mock<IHistory> _history;
        private Mock<IThings> _things;
        private Mock<ILends> _lends;

        [SetUp]
        public void Setup()
        {           
            _users = new Mock<IUsers>();
            _users.SetReturnsDefault(Task.Delay(10));
            _users.Setup(t => t.CreateUser(It.IsAny<User>())).Returns(Task.CompletedTask);
            _users.Setup(t => t.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);
            _users.Setup(t => t.DeleteUser(It.IsAny<Guid>())).Returns(Task.Delay(10));
            _users.Setup(t => t.GetUser(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new User { Id = id, Name = "Mock" }));
            _users.Setup(t => t.GetUsers())
                .Returns(Task.FromResult((new List<User>()).AsEnumerable()));
            _history = new Mock<IHistory>();
            _history.Setup(h => h.DeleteUserHistory(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _categories = new Mock<ICategories>();
            _categories.Setup(c => c.CreateCategory(It.IsAny<Guid>(),It.IsAny<Category>())).Returns(Task.CompletedTask);
            _friends = new Mock<IFriends>();
            _friends.Setup(f => f.DeleteFriends(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _things = new Mock<IThings>();
            _things.Setup(t => t.DeleteThings(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _lends = new Mock<ILends>();
            var dal = new CommonDAL(_users.Object, _friends.Object, _categories.Object, _things.Object, _lends.Object, _history.Object);
            _usersBL = new UsersBL(dal);
        }

        [Test]
        public async Task CreateUserTest()
        {
            var user = new  Models.User{ };            
            var result = await _usersBL.Create(user);
            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _usersBL.Create(null);
            });
            _users.Verify(u => u.CreateUser(It.IsAny<User>()), Times.Once());
            _categories.Verify(u => u.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()), Times.Once());
        }

        [Test]
        public async Task UpdateUserTest()
        {
            var user = new Models.User { };
            var result = await _usersBL.Update(user);
            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _usersBL.Update(null);
            });
            _users.Verify(u => u.UpdateUser(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public async Task CreateOrUpdateUserTest()
        {
            var user = new Models.User { };
            var result = await _usersBL.CreateOrUpdate(user);
            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _usersBL.CreateOrUpdate(null);
            });
            _users.Verify(u => u.CreateUser(It.IsAny<User>()), Times.AtMostOnce());
            _categories.Verify(u => u.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()), Times.AtMostOnce());
            _users.Verify(u => u.UpdateUser(It.IsAny<User>()), Times.AtMostOnce());
        }

        [Test]
        public async Task DeleteUserTest()
        {
            var user = new Models.User { };
            await _usersBL.Delete(user.Id);
            _users.Verify(u => u.DeleteUser(It.IsAny<Guid>()), Times.Once());
            _friends.Verify(u => u.DeleteFriends(It.IsAny<Guid>()), Times.Once());
            _categories.Verify(u => u.DeleteCategories(It.IsAny<Guid>()), Times.Once());
            _things.Verify(u => u.DeleteThings(It.IsAny<Guid>()), Times.Once());
            _history.Verify(u => u.DeleteUserHistory(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetUserTest()
        {
            var user = new Models.User { };
            var res = await _usersBL.Get(user.Id);
            Assert.NotNull(res);
            Assert.AreEqual(user.Id, res.Id);
            _users.Verify(u => u.GetUser(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetUsersTest()
        {
            var user = new Models.User { };
            var res = await _usersBL.GetAll();
            Assert.NotNull(res);
            _users.Verify(u => u.GetUsers(), Times.Once());
        }
    }
}