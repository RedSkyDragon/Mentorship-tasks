using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.WebAPI.Tests.Utils;

namespace ThingsBook.WebAPI.Tests
{
    [TestFixture]
    public class LendsServiceTests
    {
        private readonly Guid _guid = new Guid("12345678123456781234567812345678");

        [Test]
        public async Task AuthTest()
        {
            using (var server = TestServer.Create<TestStartupNoAuth>())
            {
                var response = await server.HttpClient.PostAsync("/lend/" + _guid, null);
                var result = await response.Content.ReadAsStringAsync();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Test]
        public async Task PostLendTest()
        {
            var values = new Dictionary<string, string>
            {
                { "FriendId", _guid.ToString() },
                { "LendDate", DateTime.Now.ToString(CultureInfo.CurrentCulture) },
                { "Comment", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PostAsync("/lend/" + _guid, new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<ThingWithLend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PutLendTest()
        {
            var values = new Dictionary<string, string>
            {
                { "FriendId", _guid.ToString() },
                { "LendDate", DateTime.Now.ToString(CultureInfo.CurrentCulture) },
                { "Comment", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PutAsync("/lend/" + _guid, new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<ThingWithLend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteLendTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync
                    ("/lend/" + _guid + "?returnDate=" + DateTime.Now.ToString(CultureInfo.CurrentCulture));
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
