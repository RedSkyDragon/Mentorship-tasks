using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class LendsTests
    {
        private IUsersDAL _users;
        private IThingsDAL _things;
        private ILendsDAL _lends;
        private Thing _thing;
        private User _user;
        private Lend _lend;
        private const string sample = "Sample";

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext("mongodb://localhost/ThingsBookTests", new MongoClient());
            _users = new UsersDAL(context);
            _user = new User { Name = sample };
            _things = new ThingsDAL(context);
            _thing = new Thing { Name = sample, About = sample, UserId = _user.Id, CategoryId = new Guid() };
            _lends = new LendsDAL(context);
            string date = "2018-08-20";
            _lend = new Lend { LendDate = DateTime.Parse(date), Comment = sample, FriendId = SequentialGuidUtils.CreateGuid() };
            await _users.CreateUser(_user);
            await _things.CreateThing(_user.Id, _thing);
        }

        [Test]
        [Explicit]
        public async Task CreateLendTest()
        {
           
            await _lends.CreateLend(_user.Id, _thing.Id, _lend);
            var dbLend = (await _things.GetThing(_user.Id, _thing.Id)).Lend;
            Assert.NotNull(dbLend);
            Assert.AreEqual(_lend.LendDate, dbLend.LendDate);
            Assert.AreEqual(_lend.Comment, dbLend.Comment);
            Assert.AreEqual(_lend.FriendId, dbLend.FriendId);
        }

        [Test]
        [Explicit]
        public async Task UpdateLendTest()
        {
            await _lends.CreateLend(_user.Id, _thing.Id, _lend);
            var updatedDate = "2018-08-21";
            _lend.LendDate = DateTime.Parse(updatedDate);
            _lend.Comment = "Updated lend";
            await _lends.UpdateLend(_user.Id, _thing.Id, _lend);
            var dbLend = (await _things.GetThing(_user.Id, _thing.Id)).Lend;
            Assert.NotNull(dbLend);
            Assert.AreEqual(_lend.LendDate, dbLend.LendDate);
            Assert.AreEqual(_lend.Comment, dbLend.Comment);
            Assert.AreEqual(_lend.FriendId, dbLend.FriendId);
        }

        [Test]
        [Explicit]
        public async Task DeleteLendTest()
        {
            var thing = new Thing { UserId = _user.Id, Name = sample };
            var lend = new Lend { LendDate = DateTime.Now, FriendId = new Guid() };
            await _things.CreateThing(_user.Id, thing);
            await _lends.CreateLend(_user.Id, thing.Id, lend);
            var dbLend = (await _things.GetThing(_user.Id, thing.Id)).Lend;
            Assert.NotNull(dbLend);
            await _lends.DeleteLend(_user.Id, thing.Id);
            dbLend = (await _things.GetThing(_user.Id, thing.Id)).Lend;
            Assert.IsNull(dbLend);
        }

        [Test]
        [Explicit]
        public async Task DeleteFriendLendsTest()
        {
            var thing1 = new Thing { UserId = _user.Id, Name = sample };
            var lend1 = new Lend { LendDate = DateTime.Now, FriendId = new Guid() };
            var thing2 = new Thing { UserId = _user.Id, Name = sample };
            var lend2 = new Lend { LendDate = DateTime.Now, FriendId = new Guid() };
            await _things.CreateThing(_user.Id, thing1);
            await _lends.CreateLend(_user.Id, thing1.Id, lend1);
            await _things.CreateThing(_user.Id, thing2);
            await _lends.CreateLend(_user.Id, thing2.Id, lend2);
            var dbLend1 = (await _things.GetThing(_user.Id, thing1.Id)).Lend;
            var dbLend2 = (await _things.GetThing(_user.Id, thing2.Id)).Lend;
            Assert.NotNull(dbLend1);
            Assert.NotNull(dbLend2);
            await _lends.DeleteFriendLends(_user.Id, new Guid());
            var dbLends = (await _things.GetThingsForFriend(_user.Id, new Guid()));
            Assert.Zero(dbLends.Count());
        }

        [TearDown]
        public async Task Final()
        {         
            await _things.DeleteThings(_user.Id);
            await _users.DeleteUser(_user.Id);
        }
    }
}
