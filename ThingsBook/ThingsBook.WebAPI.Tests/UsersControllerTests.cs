using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic;
using ThingsBook.WebAPI.Controllers;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    class UsersControllerTests
    {
        private Mock<IUsersBL> _users;

        [SetUp]
        public void SetUp()
        {
            _users = new Mock<IUsersBL>();
            UsersController controller = new UsersController(_users.Object);
        }
    }
}
