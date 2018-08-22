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

        [SetUp]
        public void Setup()
        {           
            var users = new Mock<IUsers>();
            users.SetReturnsDefault(Task.Delay(10));
            users.Setup(t => t.CreateUser(It.Is<User>(u => u != null))).Returns(Task.Delay(1));
            users.Setup(t => t.UpdateUser(It.Is<User>(u => u != null))).Returns(Task.Delay(1));
            users.Setup(t => t.DeleteUser(It.IsAny<Guid>())).Returns(Task.Delay(10));
            users.Setup(t => t.GetUser(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new User { Id = id, Name = "Mock" }));
            users.Setup(t => t.GetUsers())
                .Returns(Task.FromResult((new List<User>()).AsEnumerable()));
            var history = new Mock<IHistory>();
            history.Setup(h => h.DeleteUserHistory(It.IsAny<Guid>())).Returns(Task.Delay(1));
            var categories = new Mock<ICategories>();
            categories.Setup(c => c.CreateCategory(It.IsAny<Guid>(),It.Is<Data.Interface.Category>(u => u != null))).Returns(Task.Delay(1));
            var friends = new Mock<IFriends>();
            friends.Setup(f => f.DeleteFriends(It.IsAny<Guid>())).Returns(Task.Delay(1));
            var things = new Mock<IThings>();
            things.Setup(t => t.DeleteThings(It.IsAny<Guid>())).Returns(Task.Delay(1));
            var lends = new Mock<ILends>();
            var dal = new CommonDAL(users.Object, friends.Object, categories.Object, things.Object, lends.Object, history.Object);
            _usersBL = new UsersBL(dal);
        }

        [Test]
        public void CreateUserTest()
        {
            var user = new  Models.User{ };
            Assert.DoesNotThrowAsync(async () => {
                var result = await _usersBL.Create(user);
                Assert.NotNull(result);
                Assert.AreEqual(user.Id, result.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _usersBL.Create(null);
            });
        }

        [Test]
        public void UpdateUserTest()
        {
            var user = new Models.User { };
            Assert.DoesNotThrowAsync(async () => {
                var result = await _usersBL.Update(user);
                Assert.NotNull(result);
                Assert.AreEqual(user.Id, result.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _usersBL.Update(null);
            });
        }

        [Test]
        public void CreateOrUpdateUserTest()
        {
            var user = new Models.User { };
            Assert.DoesNotThrowAsync(async () => {
                var result = await _usersBL.CreateOrUpdate(user);
                Assert.NotNull(result);
                Assert.AreEqual(user.Id, result.Id);
            });
            Assert.ThrowsAsync<NullReferenceException>(() =>
            {
                return _usersBL.CreateOrUpdate(null);
            });
        }

        [Test]
        public void DeleteUserTest()
        {
            var user = new Models.User { };
            Assert.DoesNotThrowAsync(() => {
                return _usersBL.Delete(user.Id);
            });
        }

        [Test]
        public void GetUserTest()
        {
            var user = new Models.User { };
            Assert.DoesNotThrowAsync(async () => {
                var res = await _usersBL.Get(user.Id);
                Assert.NotNull(res);
                Assert.AreEqual(user.Id, res.Id);
            });
        }

        [Test]
        public void GetUsersTest()
        {
            var user = new Models.User { };
            Assert.DoesNotThrowAsync(async () => {
                var res = await _usersBL.GetAll();
                Assert.NotNull(res);
            });
        }
    }
}
