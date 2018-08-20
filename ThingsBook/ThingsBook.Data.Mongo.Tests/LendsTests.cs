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
    public class LendsTests
    {
        private IUsers _users;
        private IThings _things;
        private ILends _lends;
        private Thing _thing;
        private User _user;

        [SetUp]
        public async Task Setup()
        {
            var context = new ThingsBookContext("mongodb://localhost/ThingsBook");
            _users = new Users(context);
            _user = new User { Name = "LendTest User" };
            _things = new Things(context);
            _thing = new Thing { Name = "LendTest Thing", About = "LendTest About", UserId = _user.Id, CategoryId = new Guid() };
            _lends = new Lends(context);
            await _users.CreateUser(_user);
            await _things.CreateThing(_user.Id, _thing);
        }

        [TearDown]
        public async Task Final()
        {
            await _users.DeleteUser(_user.Id);
            await _things.DeleteThing(_user.Id, _thing.Id);
        }
    }
}
