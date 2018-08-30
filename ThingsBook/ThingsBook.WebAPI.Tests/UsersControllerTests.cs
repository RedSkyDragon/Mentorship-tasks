using IdentityModel;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Controllers;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    class UsersControllerTests
    {
        private Mock<IUsersBL> _users;
        private ClaimsPrincipal _user;
        private User _apiUser;

        [SetUp]
        public void SetUp()
        {
            var userId = new Guid("11111111111111111111111111111111");
            var claims = new Claim[]
            {
                new Claim(JwtClaimTypes.Id, userId.ToString()),
                new Claim(JwtClaimTypes.Name, "UserName")
            };           
            _user = new ClaimsPrincipal(Identity.Create("", claims));
            _apiUser = new User { Id = userId, Name = "UserName" };
            _users = new Mock<IUsersBL>();
            _users.Setup(u => u.Get(It.IsAny<Guid>())).Returns((Guid id) => Task.FromResult(new User { Id = id, Name = "UserName" }));
            _users.Setup(u => u.CreateOrUpdate(It.IsAny<User>())).Returns((User user) => Task.FromResult(user));
            _users.Setup(u => u.Delete(It.IsAny<Guid>())).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task GetUserTest()
        {
            Thread.CurrentPrincipal = _user;
            UsersController controller = new UsersController(_users.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
            Assert.AreEqual(_apiUser.Id, result.Id);
            Assert.AreEqual(_apiUser.Name, result.Name);
        }

        [Test]
        public async Task PostUserTest()
        {
            Thread.CurrentPrincipal = _user;
            UsersController controller = new UsersController(_users.Object);
            var result = await controller.Post();
            Assert.NotNull(result);
            Assert.AreEqual(_apiUser.Id, result.Id);
            Assert.AreEqual(_apiUser.Name, result.Name);
        }

        [Test]
        public async Task PutUserTest()
        {
            UsersController controller = new UsersController(_users.Object);
            Thread.CurrentPrincipal = _user;
            var result = await controller.Put();
            Assert.NotNull(result);
            Assert.AreEqual(_apiUser.Id, result.Id);
            Assert.AreEqual(_apiUser.Name, result.Name);
        }

        [Test]
        public Task DeleteUserTest()
        {
            Thread.CurrentPrincipal = _user;
            UsersController controller = new UsersController(_users.Object);
            return controller.Delete();
        }

        [Test]
        public void AuthorizeAttributeTest()
        {
            UsersController controller = new UsersController(_users.Object);
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();
            Assert.IsTrue(attributes.Any());
        }
    }
}
