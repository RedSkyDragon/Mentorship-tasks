using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo.Tests
{
    [TestFixture]
    public class HistoryTests
    {
        private IUsers _users;
        private IHistory _history;
        private IThings _things;
        private IFriends _friends;
        private Thing _thing;
        private HistoricalLend _lend;
        private User _user;
        private Friend _friend;

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext("mongodb://localhost/ThingsBookTests");
            _users = new Users(context);
            _user = new User { Name = "ThingsTest User" };
            _history = new History(context);
            _things = new Things(context);
            _thing = new Thing
            {
                Name = "Test Thing",
                About = "Test About",
                UserId = _user.Id,
                CategoryId = new Guid()
            };
            _friends = new Friends(context);
            _friend = new Friend { Name = "Test Friend", Contacts = "Test contacts", UserId = _user.Id };
            _lend = new HistoricalLend
            {
                LendDate = DateTime.Today,
                ReturnDate = DateTime.Today,
                Comment = "Test history",
                UserId = _user.Id,
                FriendId = _friend.Id,
                ThingId = _thing.Id
            };
            await _users.CreateUser(_user);
            await _things.CreateThing(_user.Id, _thing);
            await _friends.CreateFriend(_user.Id, _friend);
            await _history.CreateHistLend(_user.Id, _lend);
        }

        [Test]
        [Explicit]
        public async Task CreateHistoricalLendTest()
        {
            var lend = new HistoricalLend
            {
                LendDate = DateTime.Today,
                ReturnDate = DateTime.Today,
                Comment = "Test history",
                UserId = _user.Id,
                FriendId = SequentialGuidUtils.CreateGuid(),
                ThingId = SequentialGuidUtils.CreateGuid()
            };
            await _history.CreateHistLend(_user.Id, lend);
            var dbLend = await _history.GetHistLend(_user.Id, lend.Id);
            Assert.AreNotEqual(null, dbLend);
            Assert.AreEqual(lend.Id, dbLend.Id);
            Assert.AreEqual(lend.LendDate, dbLend.LendDate.ToLocalTime());
            Assert.AreEqual(lend.ReturnDate, dbLend.ReturnDate.ToLocalTime());
            Assert.AreEqual(lend.Comment, dbLend.Comment);
            Assert.AreEqual(lend.UserId, dbLend.UserId);
            Assert.AreEqual(lend.ThingId, dbLend.ThingId);
            Assert.AreEqual(lend.FriendId, dbLend.FriendId);
        }

        [Test]
        [Explicit]
        public async Task UpdateHistoricalLendTest()
        {
            _lend.ReturnDate = DateTime.Parse("2018-07-09");
            _lend.Comment = "Update";
            await _history.UpdateHistLend(_user.Id, _lend);
            var dbLend = await _history.GetHistLend(_user.Id, _lend.Id);
            Assert.AreNotEqual(null, dbLend);
            Assert.AreEqual(_lend.Id, dbLend.Id);
            Assert.AreEqual(_lend.LendDate, dbLend.LendDate.ToLocalTime());
            Assert.AreEqual(_lend.ReturnDate, dbLend.ReturnDate.ToLocalTime());
            Assert.AreEqual(_lend.Comment, dbLend.Comment);
            Assert.AreEqual(_lend.UserId, dbLend.UserId);
            Assert.AreEqual(_lend.ThingId, dbLend.ThingId);
            Assert.AreEqual(_lend.FriendId, dbLend.FriendId);
        }

        [Test]
        [Explicit]
        public async Task GetLendTest()
        {
            var lend1 = await _history.GetHistLend(_user.Id, _lend.Id);
            var lend2 = await _history.GetHistLend(_user.Id, _lend.Id);
            Assert.AreNotEqual(null, lend1);
            Assert.AreNotEqual(null, lend2);
            Assert.AreEqual(lend1.Id, lend2.Id);
            Assert.AreEqual(lend1.LendDate, lend2.LendDate);
            Assert.AreEqual(lend1.ReturnDate, lend2.ReturnDate);
            Assert.AreEqual(lend1.Comment, lend2.Comment);
            Assert.AreEqual(lend1.UserId, lend2.UserId);
            Assert.AreEqual(lend1.ThingId, lend2.ThingId);
            Assert.AreEqual(lend1.FriendId, lend2.FriendId);
        }

        [Test]
        [Explicit]
        public async Task GetLendsTest()
        {
            var lend = new HistoricalLend
            {
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now,
                Comment = "Test history",
                UserId = _user.Id,
                FriendId = SequentialGuidUtils.CreateGuid(),
                ThingId = SequentialGuidUtils.CreateGuid()
            };
            await _history.CreateHistLend(_user.Id, lend);
            var lends1 = (await _history.GetHistLends(_user.Id)).ToList();
            var lends2 = (await _history.GetHistLends(_user.Id)).ToList();
            Assert.AreEqual(lends1.Count, lends2.Count());
            var count = lends1.Count;
            for (int i = 0; i<count; i++)
            {
                Assert.AreEqual(lends1.ElementAt(i).Id, lends2.ElementAt(i).Id);
                Assert.AreEqual(lends1.ElementAt(i).LendDate, lends2.ElementAt(i).LendDate);
                Assert.AreEqual(lends1.ElementAt(i).ReturnDate, lends2.ElementAt(i).ReturnDate);
                Assert.AreEqual(lends1.ElementAt(i).Comment, lends2.ElementAt(i).Comment);
                Assert.AreEqual(lends1.ElementAt(i).UserId, lends2.ElementAt(i).UserId);
                Assert.AreEqual(lends1.ElementAt(i).ThingId, lends2.ElementAt(i).ThingId);
                Assert.AreEqual(lends1.ElementAt(i).FriendId, lends2.ElementAt(i).FriendId);
            }
        }

        [Test]
        [Explicit]
        public async Task GetFriendLendsTest()
        {
            var thing = new Thing
            {
                Name = "Test",
                About = "GetTest",
                UserId = _user.Id
            };
            var lend = new HistoricalLend
            {
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now,
                Comment = "Test history",
                UserId = _user.Id,
                FriendId = _friend.Id,
                ThingId = thing.Id
            };
            await _things.CreateThing(_user.Id, thing);
            await _history.CreateHistLend(_user.Id, lend);
            var lends1 = (await _history.GetFriendHistLends(_user.Id, _friend.Id)).ToList();
            var lends2 = (await _history.GetFriendHistLends(_user.Id, _friend.Id)).ToList();
            Assert.AreEqual(lends1.Count, lends2.Count());
            var count = lends1.Count;
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(lends1.ElementAt(i).Key.Id, lends2.ElementAt(i).Key.Id);
                Assert.AreEqual(lends1.ElementAt(i).Key.LendDate, lends2.ElementAt(i).Key.LendDate);
                Assert.AreEqual(lends1.ElementAt(i).Key.ReturnDate, lends2.ElementAt(i).Key.ReturnDate);
                Assert.AreEqual(lends1.ElementAt(i).Key.Comment, lends2.ElementAt(i).Key.Comment);
                Assert.AreEqual(lends1.ElementAt(i).Key.UserId, lends2.ElementAt(i).Key.UserId);
                Assert.AreEqual(lends1.ElementAt(i).Key.ThingId, lends2.ElementAt(i).Key.ThingId);
                Assert.AreEqual(lends1.ElementAt(i).Key.FriendId, lends2.ElementAt(i).Key.FriendId);
                Assert.AreEqual(lends1.ElementAt(i).Value.Id, lends2.ElementAt(i).Value.Id);
                Assert.AreEqual(lends1.ElementAt(i).Value.Name, lends2.ElementAt(i).Value.Name);
                Assert.AreEqual(lends1.ElementAt(i).Value.About, lends2.ElementAt(i).Value.About);
                Assert.AreEqual(lends1.ElementAt(i).Value.UserId, lends2.ElementAt(i).Value.UserId);
            }
        }

        [Test]
        [Explicit]
        public async Task GetThingLendsTest()
        {
            var friend = new Friend
            {
                Name = "Test",
                Contacts = "GetTest",
                UserId = _user.Id
            };
            var lend = new HistoricalLend
            {
                LendDate = DateTime.Now,
                ReturnDate = DateTime.Now,
                Comment = "Test history",
                UserId = _user.Id,
                FriendId = friend.Id,
                ThingId = _thing.Id
            };
            await _friends.CreateFriend(_user.Id, friend);
            await _history.CreateHistLend(_user.Id, lend);
            var lends1 = (await _history.GetThingHistLends(_user.Id, _thing.Id)).ToList();
            var lends2 = (await _history.GetThingHistLends(_user.Id, _thing.Id)).ToList();
            Assert.AreEqual(lends1.Count, lends2.Count());
            var count = lends1.Count;
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(lends1.ElementAt(i).Key.Id, lends2.ElementAt(i).Key.Id);
                Assert.AreEqual(lends1.ElementAt(i).Key.LendDate, lends2.ElementAt(i).Key.LendDate);
                Assert.AreEqual(lends1.ElementAt(i).Key.ReturnDate, lends2.ElementAt(i).Key.ReturnDate);
                Assert.AreEqual(lends1.ElementAt(i).Key.Comment, lends2.ElementAt(i).Key.Comment);
                Assert.AreEqual(lends1.ElementAt(i).Key.UserId, lends2.ElementAt(i).Key.UserId);
                Assert.AreEqual(lends1.ElementAt(i).Key.ThingId, lends2.ElementAt(i).Key.ThingId);
                Assert.AreEqual(lends1.ElementAt(i).Key.FriendId, lends2.ElementAt(i).Key.FriendId);
                Assert.AreEqual(lends1.ElementAt(i).Value.Id, lends2.ElementAt(i).Value.Id);
                Assert.AreEqual(lends1.ElementAt(i).Value.Name, lends2.ElementAt(i).Value.Name);
                Assert.AreEqual(lends1.ElementAt(i).Value.Contacts, lends2.ElementAt(i).Value.Contacts);
                Assert.AreEqual(lends1.ElementAt(i).Value.UserId, lends2.ElementAt(i).Value.UserId);
            }
        }

        [Test]
        [Explicit]
        public async Task DeleteHistLendTest()
        {
            var lend = new HistoricalLend { UserId = _user.Id };
            await _history.CreateHistLend(_user.Id, lend);
            await _history.DeleteHistLend(_user.Id, lend.Id);
            var dbLend = await _history.GetHistLend(_user.Id, lend.Id);
            Assert.AreEqual(null, dbLend);
        }

        [Test]
        [Explicit]
        public async Task DeleteHistLendsTest()
        {
            var user = new User { };
            await _users.CreateUser(user);
            var lend1 = new HistoricalLend { UserId = user.Id };
            var lend2 = new HistoricalLend { UserId = user.Id };
            await _history.CreateHistLend(user.Id, lend1);
            await _history.CreateHistLend(user.Id, lend2);
            await _history.DeleteUserHistory(user.Id);
            var dbLends = await _history.GetHistLends(user.Id);
            Assert.AreEqual(0, dbLends.Count());
            await _users.DeleteUser(user.Id);
        }

        [Test]
        [Explicit]
        public async Task DeleteFriendHistLendsTest()
        {
            var user = new User { };
            await _users.CreateUser(user);
            var lend1 = new HistoricalLend { UserId = user.Id, FriendId = SequentialGuidUtils.CreateGuid() };
            var lend2 = new HistoricalLend { UserId = user.Id, FriendId = lend1.FriendId };
            await _history.CreateHistLend(user.Id, lend1);
            await _history.CreateHistLend(user.Id, lend2);
            await _history.DeleteFriendHistory(user.Id, lend1.FriendId);
            var dbLends = await _history.GetFriendHistLends(user.Id, lend1.FriendId);
            Assert.AreEqual(0, dbLends.Count());
            await _users.DeleteUser(user.Id);
        }

        [Test]
        [Explicit]
        public async Task DeleteThingHistLendsTest()
        {
            var user = new User { };
            await _users.CreateUser(user);
            var lend1 = new HistoricalLend { UserId = user.Id, ThingId = SequentialGuidUtils.CreateGuid() };
            var lend2 = new HistoricalLend { UserId = user.Id, ThingId = lend1.ThingId };
            await _history.CreateHistLend(user.Id, lend1);
            await _history.CreateHistLend(user.Id, lend2);
            await _history.DeleteThingHistory(user.Id, lend1.ThingId);
            var dbLends = await _history.GetThingHistLends(user.Id, lend1.ThingId);
            Assert.AreEqual(0, dbLends.Count());
            await _users.DeleteUser(user.Id);
        }

        [TearDown]
        public async Task Final()
        {
            await _history.DeleteUserHistory(_user.Id);
            await _things.DeleteThings(_user.Id);
            await _friends.DeleteFriends(_user.Id);
            await _users.DeleteUser(_user.Id);
        }
    }
}
