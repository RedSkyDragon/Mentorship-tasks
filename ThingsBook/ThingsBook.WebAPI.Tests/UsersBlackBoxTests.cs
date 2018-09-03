using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Tests.Utils;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    public class UsersBlackBoxTests
    {
        [Test]
        public async Task GetUserTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = server.HttpClient.GetAsync("/user").Result;
                var result = await response.Content.ReadAsAsync<User>();
                Assert.IsNotNull(result);
            }
        }
    }
}
