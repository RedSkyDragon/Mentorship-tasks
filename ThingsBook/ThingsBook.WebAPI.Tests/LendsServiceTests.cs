using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public async Task PostLendTest()
        {
            var values = new Dictionary<string, string>
            {
                { "FriendId", _guid.ToString() },
                { "LendDate", DateTime.Now.ToString() },
                { "Comment", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PostAsync("/lend/" + _guid.ToString(), new FormUrlEncodedContent(values));
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
                { "LendDate", DateTime.Now.ToString() },
                { "Comment", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PutAsync("/lend/" + _guid.ToString(), new FormUrlEncodedContent(values));
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
                    ("/lend/" + _guid.ToString() + "?returnDate=" + DateTime.Now.ToString());
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
