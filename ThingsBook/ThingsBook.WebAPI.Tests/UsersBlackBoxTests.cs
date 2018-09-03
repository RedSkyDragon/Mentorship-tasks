using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.WebAPI.Tests.Utils;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    class UsersBlackBoxTests
    {
        [Test]
        public void GetUserTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                Assert.IsNotNull(server);
                //var response = server.HttpClient.GetAsync("/user").Result;
                //var result = await response.Content.ReadAsStringAsync();
                //Assert.IsNotNull(result);
            }
        }
    }
}
