using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Tests.Utils;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        [Test]
        public async Task GetUserTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/user");
                var result = await response.Content.ReadAsAsync<User>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PostUserTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PostAsync("/user", null);
                var result = await response.Content.ReadAsAsync<User>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PutUserTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PutAsync("/user", null);
                var result = await response.Content.ReadAsAsync<User>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteUserTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync("/user");
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
