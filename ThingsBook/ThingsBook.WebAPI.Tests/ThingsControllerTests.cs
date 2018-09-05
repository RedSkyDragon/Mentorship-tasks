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
    public class ThingsControllerTests
    {
        private Mock<IThingsBL> _things;
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
            _things = new Mock<IThingsBL>();
            _things
                .Setup(t => t.CreateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()))
                .Returns((Guid id, ThingWithLend th) => Task.FromResult(th));
            _things
                .Setup(t => t.UpdateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()))
                .Returns((Guid id, ThingWithLend th) => Task.FromResult(th));
            _things
                .Setup(t => t.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid th) => Task.FromResult(new ThingWithLend { Id = th }));
            _things
                .Setup(t => t.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things
                .Setup(t => t.GetThingLends(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new FilteredLends()));
            _things
                .Setup(t => t.GetThings(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ThingWithLend>() as IEnumerable<ThingWithLend>));
        }

        [Test]
        public async Task GetThingsTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new ThingsController(_things.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
            _things.Verify(u => u.GetThings(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetThingTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new ThingsController(_things.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
            _things.Verify(u => u.GetThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task PostThingTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new ThingsController(_things.Object);
            var result = await controller.Post(new Models.Thing { Name = Sample, About = Sample });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Name);
            Assert.AreEqual(Sample, result.About);
            _things.Verify(u => u.CreateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()), Times.Once());
        }

        [Test]
        public async Task PutThingTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new ThingsController(_things.Object);
            var result = await controller.Put(new Guid(), new Models.Thing { Name = Sample, About = Sample });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Name);
            Assert.AreEqual(Sample, result.About);
            _things.Verify(u => u.UpdateThing(It.IsAny<Guid>(), It.IsAny<ThingWithLend>()), Times.Once());
        }

        [Test]
        public async Task GetThingLendsTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new ThingsController(_things.Object);
            var result = await controller.GetLends(new Guid());
            Assert.NotNull(result);
            _things.Verify(u => u.GetThingLends(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task DeleteThingTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new ThingsController(_things.Object);
            await controller.Delete(new Guid());
            _things.Verify(u => u.DeleteThing(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }
    }
}
