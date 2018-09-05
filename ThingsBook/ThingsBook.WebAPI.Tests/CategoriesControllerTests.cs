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
    public class CategoriesControllerTests
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
                .Setup(t => t.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns((Guid id, Category cat) => Task.FromResult(cat));
            _things
                .Setup(t => t.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns((Guid id, Category cat) => Task.FromResult(cat));
            _things
                .Setup(t => t.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid cat) => Task.FromResult(new Category { Id = cat }));
            _things
                .Setup(t => t.DeleteCategoryWithThings(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things
                .Setup(t => t.DeleteCategoryWithReplacement(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things
                .Setup(t => t.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ThingWithLend>() as IEnumerable<ThingWithLend>));
            _things
                .Setup(t => t.GetCategories(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Category>() as IEnumerable<Category>));
        }

        [Test]
        public async Task GetCategoriesTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
            _things.Verify(u => u.GetCategories(It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task GetCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
            _things.Verify(u => u.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task PostCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            var result = await controller.Post(new Models.Category { Name = Sample, About = Sample });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Name);
            Assert.AreEqual(Sample, result.About);
            _things.Verify(u => u.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()), Times.Once());
        }

        [Test]
        public async Task PutCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            var result = await controller.Put(new Guid(), new Models.Category { Name = Sample, About = Sample });
            Assert.NotNull(result);
            Assert.AreEqual(Sample, result.Name);
            Assert.AreEqual(Sample, result.About);
            _things.Verify(u => u.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()), Times.Once());
        }

        [Test]
        public async Task GetThingsTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            var result = await controller.GetForCategory(new Guid());
            Assert.NotNull(result);
            _things.Verify(u => u.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task DeleteCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            await controller.Delete(new Guid());
            _things.Verify(u => u.DeleteCategoryWithThings(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        [Test]
        public async Task DeleteAndReplaceTest()
        {
            Thread.CurrentPrincipal = _user;
            var controller = new CategoriesController(_things.Object);
            await controller.DeleteAndReplace(new Guid(), new Guid());
            _things.Verify(u => u.DeleteCategoryWithReplacement(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }
    }
}
