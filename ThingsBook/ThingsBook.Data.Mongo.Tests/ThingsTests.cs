﻿using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class ThingsTests
    {
        private IUsersDAL _users;
        private IThingsDAL _things;
        private ICategoriesDAL _categories;
        private Thing _thing;
        private User _user;
        private const string Sample = "Sample";

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString, new MongoClient());
            _users = new UsersDAL(context);
            _user = new User { Name = Sample };
            _things = new ThingsDAL(context);
            _categories = new CategoriesDAL(context);
            _thing = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = new Guid()
            };
            await _users.CreateUser(_user);
            await _things.CreateThing(_user.Id, _thing);
        }

        [Test]
        [Explicit]
        public async Task CreateThingTest()
        {
            var thing = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = SequentialGuidUtils.CreateGuid()
            };
            await _things.CreateThing(_user.Id, thing);
            var dbThing = await _things.GetThing(_user.Id, thing.Id);
            Assert.NotNull(dbThing);
            Assert.AreEqual(thing.Id, dbThing.Id);
            Assert.AreEqual(thing.Name, dbThing.Name);
            Assert.AreEqual(thing.UserId, dbThing.UserId);
            Assert.AreEqual(thing.About, dbThing.About);
            Assert.AreEqual(thing.CategoryId, dbThing.CategoryId);
        }

        [Test]
        [Explicit]
        public async Task UpdateThingTest()
        {
            _thing.Name = "Updated";
            _thing.About = "Updated";
            _thing.CategoryId = SequentialGuidUtils.CreateGuid();
            await _things.UpdateThing(_user.Id, _thing);
            var updated = await _things.GetThing(_user.Id, _thing.Id);
            Assert.NotNull(updated);
            Assert.AreEqual(_thing.Id, updated.Id);
            Assert.AreEqual(_thing.Name, updated.Name);
            Assert.AreEqual(_thing.UserId, updated.UserId);
            Assert.AreEqual(_thing.About, updated.About);
            Assert.AreEqual(_thing.CategoryId, updated.CategoryId);
        }

        [Test]
        [Explicit]
        public async Task UpdateThingsCategoryTest()
        {
            var catId = SequentialGuidUtils.CreateGuid();
            var thing1 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = catId
            };
            var thing2 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = catId
            };
            await _things.CreateThing(_user.Id, thing1);
            await _things.CreateThing(_user.Id, thing2);
            var update = SequentialGuidUtils.CreateGuid();
            await _things.UpdateThingsCategory(_user.Id, catId, update);
            var updated = await _things.GetThingsForCategory(_user.Id, update);
            Assert.NotNull(updated);
            Assert.IsTrue(updated.All(u => u.CategoryId == update));
        }

        [Test]
        [Explicit]
        public async Task DeleteThingTest()
        {
            var thing = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = SequentialGuidUtils.CreateGuid()
            };
            await _things.CreateThing(_user.Id, thing);
            var dbThing = await _things.GetThing(_user.Id, thing.Id);
            Assert.NotNull(dbThing);
            await _things.DeleteThing(_user.Id, thing.Id);
            dbThing = await _things.GetThing(_user.Id, thing.Id);
            Assert.Null(dbThing);
        }

        [Test]
        [Explicit]
        public async Task DeleteThingsForCategoryTest()
        {
            var cat = new Category { Name = Sample, About = Sample, UserId = _user.Id };
            await _categories.CreateCategory(_user.Id, cat);
            var thing1 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = cat.Id
            };
            var thing2 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = cat.Id
            };
            await _things.CreateThing(_user.Id, thing1);
            await _things.CreateThing(_user.Id, thing2);
            var dbThings = await _things.GetThingsForCategory(_user.Id, cat.Id);
            Assert.NotZero(dbThings.Count());
            await _things.DeleteThingsForCategory(_user.Id, cat.Id);
            dbThings = await _things.GetThingsForCategory(_user.Id, cat.Id);
            Assert.Zero(dbThings.Count());
        }

        [Test]
        [Explicit]
        public async Task DeleteThingsTest()
        {
            var user = new User();
            var thing1 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = user.Id,
                CategoryId = new Guid()
            };
            var thing2 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = user.Id,
                CategoryId = new Guid()
            };
            await _things.CreateThing(user.Id, thing1);
            await _things.CreateThing(user.Id, thing2);
            var dbThings = await _things.GetThings(user.Id);
            Assert.NotZero(dbThings.Count());
            await _things.DeleteThings(user.Id);
            dbThings = await _things.GetThings(user.Id);
            Assert.Zero(dbThings.Count());
            await _users.DeleteUser(user.Id);
        }

        [Test]
        [Explicit]
        public async Task GetThingTest()
        {
            var first = await _things.GetThing(_user.Id, _thing.Id);
            var second = await _things.GetThing(_user.Id, _thing.Id);
            Assert.NotNull(first);
            Assert.AreEqual(first.Id, second.Id);
            Assert.AreEqual(first.Name, second.Name);
            Assert.AreEqual(first.About, second.About);
            Assert.AreEqual(first.UserId, second.UserId);
            Assert.AreEqual(first.CategoryId, second.CategoryId);
        }

        [Test]
        [Explicit]
        public async Task GetThingsTest()
        {
            var thing = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = new Guid()
            };
            await _things.CreateThing(_user.Id, thing);
            var first = (await _things.GetThings(_user.Id)).ToList();
            var second = (await _things.GetThings(_user.Id)).ToList();
            Assert.AreEqual(first.Count, second.Count);
            var count = first.Count;
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(first.ElementAt(i).Id, second.ElementAt(i).Id);
                Assert.AreEqual(first.ElementAt(i).Name, second.ElementAt(i).Name);
                Assert.AreEqual(first.ElementAt(i).About, second.ElementAt(i).About);
                Assert.AreEqual(first.ElementAt(i).UserId, second.ElementAt(i).UserId);
                Assert.AreEqual(first.ElementAt(i).CategoryId, second.ElementAt(i).CategoryId);
            }        
        }

        [Test]
        [Explicit]
        public async Task GetThingsForCategoryTest()
        {
            var catId = SequentialGuidUtils.CreateGuid();
            var thing1 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = catId
            };
            var thing2 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                CategoryId = catId
            };
            await _things.CreateThing(_user.Id, thing1);
            await _things.CreateThing(_user.Id, thing2);
            var first = (await _things.GetThingsForCategory(_user.Id, catId)).ToList();
            var second = (await _things.GetThingsForCategory(_user.Id, catId)).ToList();
            Assert.AreEqual(first.Count, second.Count);
            for (int i = 0; i < first.Count; i++)
            {
                Assert.AreEqual(first.ElementAt(i).Id, second.ElementAt(i).Id);
                Assert.AreEqual(first.ElementAt(i).Name, second.ElementAt(i).Name);
                Assert.AreEqual(first.ElementAt(i).About, second.ElementAt(i).About);
                Assert.AreEqual(first.ElementAt(i).UserId, second.ElementAt(i).UserId);
                Assert.AreEqual(first.ElementAt(i).CategoryId, second.ElementAt(i).CategoryId);
            }
        }

        [Test]
        [Explicit]
        public async Task GetThingsForFriendTest()
        {
            var friendId = SequentialGuidUtils.CreateGuid();
            var lend = new Lend { FriendId = friendId };
            var thing1 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                Lend = lend,
                CategoryId = new Guid()
            };
            var thing2 = new Thing
            {
                Name = Sample,
                About = Sample,
                UserId = _user.Id,
                Lend = lend,
                CategoryId = new Guid()
            };
            await _things.CreateThing(_user.Id, thing1);
            await _things.CreateThing(_user.Id, thing2);
            var first = (await _things.GetThingsForFriend(_user.Id, friendId)).ToList();
            var second = (await _things.GetThingsForFriend(_user.Id, friendId)).ToList();
            Assert.AreEqual(first.Count, second.Count);
            for (int i = 0; i < first.Count; i++)
            {
                Assert.AreEqual(first.ElementAt(i).Id, second.ElementAt(i).Id);
                Assert.AreEqual(first.ElementAt(i).Name, second.ElementAt(i).Name);
                Assert.AreEqual(first.ElementAt(i).About, second.ElementAt(i).About);
                Assert.AreEqual(first.ElementAt(i).UserId, second.ElementAt(i).UserId);
                Assert.AreEqual(first.ElementAt(i).CategoryId, second.ElementAt(i).CategoryId);
            }
        }

        [TearDown]
        public async Task Final()
        {
            await _things.DeleteThings(_user.Id);
            await _users.DeleteUser(_user.Id);
        }
    }
}
