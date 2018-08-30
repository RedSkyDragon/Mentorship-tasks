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
    class CategoriesControllerTests
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
            _things.Setup(t => t.CreateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns((Guid id, Category cat) => Task.FromResult(cat));
            _things.Setup(t => t.UpdateCategory(It.IsAny<Guid>(), It.IsAny<Category>()))
                .Returns((Guid id, Category cat) => Task.FromResult(cat));
            _things.Setup(t => t.GetCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid id, Guid cat) => Task.FromResult(new Category { Id = cat }));
            _things.Setup(t => t.DeleteCategoryWithThings(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things.Setup(t => t.DeleteCategoryWithReplacement(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
            _things.Setup(t => t.GetThingsForCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<ThingWithLend>() as IEnumerable<ThingWithLend>));
            _things.Setup(t => t.GetCategories(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<Category>() as IEnumerable<Category>));
        }

        [Test]
        public async Task GetCategoriesTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            var result = await controller.Get();
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            var result = await controller.Get(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public async Task PostCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            var result = await controller.Post(new Models.Category { Name = sample, About = sample });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Name);
            Assert.AreEqual(sample, result.About);
        }

        [Test]
        public async Task PutCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            var result = await controller.Put(new Guid(), new Models.Category { Name = sample, About = sample });
            Assert.NotNull(result);
            Assert.AreEqual(sample, result.Name);
            Assert.AreEqual(sample, result.About);
        }

        [Test]
        public async Task GetThingsTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            var result = await controller.GetForCategory(new Guid());
            Assert.NotNull(result);
        }

        [Test]
        public Task DeleteCategoryTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            return controller.Delete(new Guid());
        }

        [Test]
        public Task DeleteAndReplaceTest()
        {
            Thread.CurrentPrincipal = _user;
            CategoriesController controller = new CategoriesController(_things.Object);
            return controller.DeleteAndReplace(new Guid(), new Guid());
        }

        [Test]
        public void AuthorizeAttributeTest()
        {
            CategoriesController controller = new CategoriesController(_things.Object);
            var type = controller.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true).ToList();
            Assert.IsTrue(attributes.Any());
        }
    }
}
