using IdentityModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    class FriendsControllerTests
    {
        private Mock<IFriendsBL> _friends;
        private ClaimsPrincipal _user;
        private User _apiUser;
        private const string sample = "Sample";

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
            _friends = new Mock<IFriendsBL>();
            _friends.Setup(t => t.Create(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns((Guid id, Friend fr) => Task.FromResult(fr));
            _friends.Setup(t => t.Update(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns((Guid id, Friend fr) => Task.FromResult(fr));
            _friends.Setup(t => t.GetOne(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid fr) => Task.FromResult(new Friend { Id = fr }));
            _friends.Setup(t => t.Delete(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _friends.Setup(t => t.GetFriendLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new FilteredLends()));
            _friends.Setup(t => t.GetAll(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Friend>() as IEnumerable<Friend>));
        }

        [Test]
        public async Task GetFriendsTest()
        {
            Thread.CurrentPrincipal = _user;
            FriendsController controller = new FriendsController(_friends.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            FriendsController controller = new FriendsController(_friends.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public async Task PostFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            FriendsController controller = new FriendsController(_friends.Object);
            var result = await controller.Post(new Models.Friend { Name = sample, Contacts = sample });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Name);
            Assert.AreEqual(sample, result.Contacts);
        }

        [Test]
        public async Task PutFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            FriendsController controller = new FriendsController(_friends.Object);
            var result = await controller.Put(new Guid(), new Models.Friend { Name = sample, Contacts = sample });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Name);
            Assert.AreEqual(sample, result.Contacts);
        }

        [Test]
        public async Task GetThingsTest()
        {
            Thread.CurrentPrincipal = _user;
            FriendsController controller = new FriendsController(_friends.Object);
            var result = await controller.GetLends(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public Task DeleteFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            FriendsController controller = new FriendsController(_friends.Object);
            return controller.Delete(new Guid());
        }

        [Test]
        public void AuthorizeAttributeTest()
        {
            FriendsController controller = new FriendsController(_friends.Object);
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();
            Assert.IsTrue(attributes.Any());
        }
    }
}
