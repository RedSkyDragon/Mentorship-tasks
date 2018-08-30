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
    class LendsControllerTests
    {
        private Mock<ILendsBL> _lends;
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
            _lends = new Mock<ILendsBL>();
            _lends.Setup(t => t.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns((Guid id, Guid th, Lend lend) => Task.FromResult(new ThingWithLend { Id = th, Lend = lend }));
            _lends.Setup(t => t.Update(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Lend>()))
                .Returns((Guid id, Guid th, Lend lend) => Task.FromResult(new ThingWithLend { Id = th, Lend = lend }));
            _lends.Setup(t => t.Delete(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);
        }

        [Test]
        public async Task PostLendTest()
        {
            Thread.CurrentPrincipal = _user;
            LendsController controller = new LendsController(_lends.Object);
            var date = DateTime.Now - TimeSpan.FromDays(1);
            var result = await controller.Post(new Guid(), new Lend { FriendId = new Guid(), Comment = sample, LendDate = date });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Lend.Comment);
            Assert.AreEqual(date, result.Lend.LendDate);
        }

        [Test]
        public async Task PutLendTest()
        {
            Thread.CurrentPrincipal = _user;
            LendsController controller = new LendsController(_lends.Object);
            var date = DateTime.Now - TimeSpan.FromDays(1);
            var result = await controller.Put(new Guid(), new Lend { FriendId = new Guid(), Comment = sample, LendDate = date });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Lend.Comment);
            Assert.AreEqual(date, result.Lend.LendDate);
        }

        [Test]
        public Task DeleteLendTest()
        {
            Thread.CurrentPrincipal = _user;
            LendsController controller = new LendsController(_lends.Object);
            var date = DateTime.Now;
            return controller.Delete(new Guid(), date);
        }

        [Test]
        public void AuthorizeAttributeTest()
        {
            LendsController controller = new LendsController(_lends.Object);
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();
            Assert.IsTrue(attributes.Any());
        }
    }
}
