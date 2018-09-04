using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Tests.Utils;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    public class FriendsServiceTests
    {
        private readonly Guid _guid = new Guid("12345678123456781234567812345678");

        [Test]
        public async Task AuthTest()
        {
            using (var server = TestServer.Create<TestStartupWithoutAuth>())
            {
                var response = await server.HttpClient.GetAsync("/friends");
                var result = await response.Content.ReadAsStringAsync();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Test]
        public async Task GetFriendsTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/friends");
                var result = await response.Content.ReadAsAsync<IEnumerable<Friend>>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetFriendTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/friend/" + _guid.ToString());
                var result = await response.Content.ReadAsAsync<Friend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetLendsTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/friend/" + _guid.ToString() + "/lends");
                var result = await response.Content.ReadAsAsync<FilteredLends>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PostFriendTest()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "Sample" },
                { "Contacts", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PostAsync("/friend", new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<Friend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PutFriendTest()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "Sample" },
                { "Contacts", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PutAsync("/friend/" + _guid.ToString(), new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<Friend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteFriendTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync("/friend/" + _guid.ToString());
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
