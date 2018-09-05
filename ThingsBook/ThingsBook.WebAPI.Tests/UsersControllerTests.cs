using IdentityModel;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Controllers;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUsersBL> _users;
        private ClaimsPrincipal _user;
        private User _apiUser;

        [SetUp]
        public void SetUp()
        {
            var userId = new Guid("11111111111111111111111111111111");
            var claims = new[]
            {
                new Claim(JwtClaimTypes.Id, userId.ToString()),
                new Claim(JwtClaimTypes.Name, "UserName")
            };           
            _user = new ClaimsPrincipal(Identity.Create("", claims));
            _apiUser = new User { Id = userId, Name = "UserName" };
            _users = new Mock<IUsersBL>();
            _users
                .Setup(u => u.Get(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new User { Id = id, Name = "UserName" }));
            _users
                .Setup(u => u.CreateOrUpdate(It.IsAny<User>()))
                .Returns((User user) => Task.FromResult(user));
            _users
                .Setup(u => u.Delete(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
        }

        [Test]
        public async Task GetUserTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new UsersController(_users.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
            Assert.AreEqual(_apiUser.Id, result.Id);
            Assert.AreEqual(_apiUser.Name, result.Name);
            _users.Verify(u => u.Get(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task PostUserTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new UsersController(_users.Object);
            var result = await controller.Post();
            Assert.NotNull(result);
            Assert.AreEqual(_apiUser.Id, result.Id);
            Assert.AreEqual(_apiUser.Name, result.Name);
            _users.Verify(u => u.CreateOrUpdate(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public async Task PutUserTest()
        {
            var controller = new UsersController(_users.Object);
            Thread.CurrentPrincipal = _user;
            var result = await controller.Put();
            Assert.NotNull(result);
            Assert.AreEqual(_apiUser.Id, result.Id);
            Assert.AreEqual(_apiUser.Name, result.Name);
            _users.Verify(u => u.CreateOrUpdate(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public async Task DeleteUserTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new UsersController(_users.Object);
            await controller.Delete();
            _users.Verify(u => u.Delete(It.IsAny<Guid>()), Times.Once());
        }
    }
}
