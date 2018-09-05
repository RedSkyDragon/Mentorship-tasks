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
    public class ThingsServiceTests
    {
        private readonly Guid _guid = new Guid("12345678123456781234567812345678");

        [Test]
        public async Task AuthTest()
        {
            using (var server = TestServer.Create<TestStartupNoAuth>())
            {
                var response = await server.HttpClient.GetAsync("/things");
                var result = await response.Content.ReadAsStringAsync();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Test]
        public async Task GetThingsTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/things");
                var result = await response.Content.ReadAsAsync<IEnumerable<ThingWithLend>>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetThingTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/thing/" + _guid);
                var result = await response.Content.ReadAsAsync<ThingWithLend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetLendsTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/thing/" + _guid + "/lends");
                var result = await response.Content.ReadAsAsync<FilteredLends>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PostThingTest()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "Sample" },
                { "About", "Sample" },
                { "CategoryId", _guid.ToString() }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PostAsync("/thing", new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<ThingWithLend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PutThingTest()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "Sample" },
                { "About", "Sample" },
                { "CategoryId", _guid.ToString() }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PutAsync("/thing/" + _guid, new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<ThingWithLend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteThingTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync("/thing/" + _guid);
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
