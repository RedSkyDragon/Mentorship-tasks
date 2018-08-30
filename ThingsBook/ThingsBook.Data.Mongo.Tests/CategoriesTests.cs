﻿using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class CategoriesTests
    {
        private ICategories _categories;
        private IUsers _users;
        private User _user;
        private Category _category;

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext("mongodb://localhost/ThingsBookTests");
            _users = new Users(context);
            _user = new User { Name = "CategoryTest User" };
            _categories = new Categories(context);
            _category = new Category { Name = "Test Category", About = "Test contacts", UserId = _user.Id };
            await _users.CreateUser(_user);
            await _categories.CreateCategory(_user.Id, _category);
        }

        [Test]
        [Explicit]
        public async Task CreateCategoryTest()
        {
            var category = new Category { Name = "Test Category Create", About = "Test contacts", UserId = _user.Id };
            await _categories.CreateCategory(_user.Id, category);
            var dbCategory = await _categories.GetCategory(_user.Id, category.Id);
            Assert.AreEqual(category.Id, dbCategory.Id);
            Assert.AreEqual(category.Name, dbCategory.Name);
            Assert.AreEqual(category.About, dbCategory.About);
            Assert.AreEqual(category.UserId, dbCategory.UserId);
            await _categories.DeleteCategory(_user.Id, category.Id);
        }

        [Test]
        [Explicit]
        public async Task GetCategoryTest()
        {
            var first = await _categories.GetCategory(_user.Id, _category.Id);
            var second = await _categories.GetCategory(_user.Id, _category.Id);
            Assert.NotNull(first);
            Assert.AreEqual(first.Id, second.Id);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.About, second.About);
            Assert.AreEqual(first.UserId, second.UserId);
        }

        [Test]
        [Explicit]
        public async Task GetAllCategoriesTest()
        {
            var categories = await _categories.GetCategories(_user.Id);
            Assert.True(categories.Count() > 0);
        }

        [Test]
        [Explicit]
        public async Task UpdateCategoryTest()
        {
            _category.Name = "Updated name";
            _category.About = "Updated contacts";
            await _categories.UpdateCategory(_user.Id, _category);
            var updated = await _categories.GetCategory(_user.Id, _category.Id);
            Assert.AreEqual(_category.Id, updated.Id);
            Assert.AreEqual(_category.Name, updated.Name);
            Assert.AreEqual(_category.About, updated.About);
            Assert.AreEqual(_category.UserId, updated.UserId);
        }

        [Test]
        [Explicit]
        public async Task DeleteCategoryTest()
        {
            var category = new Category { Name = "Test Category Delete", About = "Test contacts", UserId = _user.Id };
            await _categories.CreateCategory(_user.Id, category);
            await _categories.DeleteCategory(_user.Id, category.Id);
            var dbCategory = await _categories.GetCategory(_user.Id, category.Id);
            Assert.Null(dbCategory);
        }

        [Test]
        [Explicit]
        public async Task DeleteCategoriesTest()
        {
            var user = new User { Name = "Delete Test" };
            await _users.CreateUser(user);
            var category1 = new Category { Name = "Test Category Delete", About = "Test contacts", UserId = user.Id };
            var category2 = new Category { Name = "Test Category Delete", About = "Test contacts", UserId = user.Id };
            await _categories.CreateCategory(user.Id, category1);
            await _categories.CreateCategory(user.Id, category2);
            await _categories.DeleteCategories(user.Id);
            var categories = (await _categories.GetCategories(user.Id)).ToList();
            Assert.True(categories.Count() == 0);
            await _users.DeleteUser(user.Id);
        }

        [TearDown]
        public async Task Final()
        {
            await _users.DeleteUser(_user.Id);
            await _categories.DeleteCategory(_user.Id, _category.Id);
        }
    }
}