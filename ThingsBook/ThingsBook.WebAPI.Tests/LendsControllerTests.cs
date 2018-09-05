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
    public class LendsControllerTests
    {
        private Mock<ILendsBL> _lends;
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
            _lends = new Mock<ILendsBL>();
            _lends
                .Setup(t => t.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns((Guid id, Guid th, Lend lend) => Task.FromResult(new ThingWithLend { Id = th, Lend = lend }));
            _lends
                .Setup(t => t.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns((Guid id, Guid th, Lend lend) => Task.FromResult(new ThingWithLend { Id = th, Lend = lend }));
            _lends
                .Setup(t => t.Delete(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);
        }

        [Test]
        public async Task PostLendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new LendsController(_lends.Object);
            var date = DateTime.Now - TimeSpan.FromDays(1);
            var result = await controller.Post(new Guid(), new Lend { FriendId = new Guid(), Comment = Sample, LendDate = date });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Lend.Comment);
            Assert.AreEqual(date, result.Lend.LendDate);
            _lends.Verify(l => l.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()), Times.Once());
        }

        [Test]
        public async Task PutLendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new LendsController(_lends.Object);
            var date = DateTime.Now - TimeSpan.FromDays(1);
            var result = await controller.Put(new Guid(), new Lend { FriendId = new Guid(), Comment = Sample, LendDate = date });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Lend.Comment);
            Assert.AreEqual(date, result.Lend.LendDate);
            _lends.Verify(l => l.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()), Times.Once());
        }

        [Test]
        public async Task DeleteLendTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new LendsController(_lends.Object);
            var date = DateTime.Now;
            await controller.Delete(new Guid(), date);
            _lends.Verify(l => l.Delete(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<DateTime>()), Times.Once());
        }
    }
}
