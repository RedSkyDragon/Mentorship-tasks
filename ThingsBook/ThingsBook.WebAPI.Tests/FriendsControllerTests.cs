using IdentityModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Controllers;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    public class FriendsControllerTests
    {
        private Mock<IFriendsBL> _friends;
        private ClaimsPrincipal _user;
        private const string Sample = "Sample";

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
            _friends = new Mock<IFriendsBL>();
            _friends
                .Setup(t => t.Create(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns((Guid id, Friend fr) => Task.FromResult(fr));
            _friends
                .Setup(t => t.Update(It.IsAny<Guid>(), It.IsAny<Friend>()))
                .Returns((Guid id, Friend fr) => Task.FromResult(fr));
            _friends
                .Setup(t => t.GetOne(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid fr) => Task.FromResult(new Friend { Id = fr }));
            _friends
                .Setup(t => t.Delete(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _friends
                .Setup(t => t.GetFriendLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new FilteredLends()));
            _friends
                .Setup(t => t.GetAll(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Friend>() as IEnumerable<Friend>));
        }

        [Test]
        public async Task GetFriendsTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new FriendsController(_friends.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
            _friends.Verify(f => f.GetAll(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new FriendsController(_friends.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
            _friends.Verify(f => f.GetOne(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task PostFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new FriendsController(_friends.Object);
            var result = await controller.Post(new Models.Friend { Name = Sample, Contacts = Sample });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Name);
            Assert.AreEqual(Sample, result.Contacts);
            _friends.Verify(f => f.Create(It.IsAny<Guid>(), It.IsAny<Friend>()), Times.Once());
        }

        [Test]
        public async Task PutFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new FriendsController(_friends.Object);
            var result = await controller.Put(new Guid(), new Models.Friend { Name = Sample, Contacts = Sample });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Name);
            Assert.AreEqual(Sample, result.Contacts);
            _friends.Verify(f => f.Update(It.IsAny<Guid>(), It.IsAny<Friend>()), Times.Once());
        }

        [Test]
        public async Task GetLendsTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new FriendsController(_friends.Object);
            var result = await controller.GetLends(new Guid());
            Assert.NotNull(result);
            _friends.Verify(f => f.GetFriendLends(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task DeleteFriendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new FriendsController(_friends.Object);
            await controller.Delete(new Guid());
            _friends.Verify(f => f.Delete(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }
    }
}
