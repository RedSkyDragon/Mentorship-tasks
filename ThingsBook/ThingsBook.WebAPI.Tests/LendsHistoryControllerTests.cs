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
    class LendsHistoryControllerTests
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
            _lends.Setup(t => t.GetHistoricalLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid hid) => Task.FromResult(new HistLend { Id = hid }));
            _lends.Setup(t => t.GetHistoricalLends(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new List<HistLend>() as IEnumerable<HistLend>));
            _lends.Setup(t => t.DeleteHistoricalLend(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
        }

        [Test]
        public async Task GetTest()
        {
            Thread.CurrentPrincipal = _user;
            LendsHistoryController controller = new LendsHistoryController(_lends.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAllTest()
        {
            Thread.CurrentPrincipal = _user;
            LendsHistoryController controller = new LendsHistoryController(_lends.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
        }

        [Test]
        public Task DeleteLendTest()
        {
            Thread.CurrentPrincipal = _user;
            LendsHistoryController controller = new LendsHistoryController(_lends.Object);
            return controller.Delete(new Guid());
        }

        [Test]
        public void AuthorizeAttributeTest()
        {
            LendsHistoryController controller = new LendsHistoryController(_lends.Object);
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();
            Assert.IsTrue(attributes.Any());
        }
    }
}
