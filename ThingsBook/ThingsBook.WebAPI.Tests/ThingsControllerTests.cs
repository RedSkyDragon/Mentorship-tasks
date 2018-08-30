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
    class ThingsControllerTests
    {
        private Mock<IThingsBL> _things;
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
            _things = new Mock<IThingsBL>();
            _things.Setup(t => t.CreateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()))
                .Returns((Guid id, ThingWithLend th) => Task.FromResult(th));
            _things.Setup(t => t.UpdateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()))
                .Returns((Guid id, ThingWithLend th) => Task.FromResult(th));
            _things.Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid th) => Task.FromResult(new ThingWithLend { Id = th }));
            _things.Setup(t => t.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things.Setup(t => t.GetThingLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new FilteredLends()));
            _things.Setup(t => t.GetThings(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ThingWithLend>() as IEnumerable<ThingWithLend>));
        }

        [Test]
        public async Task GetThingsTest()
        {
            Thread.CurrentPrincipal = _user;
            ThingsController controller = new ThingsController(_things.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetThingTest()
        {
            Thread.CurrentPrincipal = _user;
            ThingsController controller = new ThingsController(_things.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public async Task PostThingTest()
        {
            Thread.CurrentPrincipal = _user;
            ThingsController controller = new ThingsController(_things.Object);
            var result = await controller.Post(new Models.Thing { Name = sample, About = sample });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Name);
            Assert.AreEqual(sample, result.About);
        }

        [Test]
        public async Task PutThingTest()
        {
            Thread.CurrentPrincipal = _user;
            ThingsController controller = new ThingsController(_things.Object);
            var result = await controller.Put(new Guid(), new Models.Thing { Name = sample, About = sample });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Name);
            Assert.AreEqual(sample, result.About);
        }

        [Test]
        public async Task GetThingLendsTest()
        {
            Thread.CurrentPrincipal = _user;
            ThingsController controller = new ThingsController(_things.Object);
            var result = await controller.GetLends(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public Task DeleteThingTest()
        {
            Thread.CurrentPrincipal = _user;
            ThingsController controller = new ThingsController(_things.Object);
            return controller.Delete(new Guid());
        }

        [Test]
        public void AuthorizeAttributeTest()
        {
            ThingsController controller = new ThingsController(_things.Object);
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();
            Assert.IsTrue(attributes.Any());
        }
    }
}
