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
    public class CategoriesServiceTests
    {
        private readonly Guid _guid = new Guid("12345678123456781234567812345678");

        [Test]
        public async Task AuthTest()
        {
            using (var server = TestServer.Create<TestStartupWithoutAuth>())
            {
                var response = await server.HttpClient.GetAsync("/categories");
                var result = await response.Content.ReadAsStringAsync();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Test]
        public async Task GetCategoriesTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/categories");
                var result = await response.Content.ReadAsAsync<IEnumerable<Category>>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetCategoryTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/category/" + _guid.ToString());
                var result = await response.Content.ReadAsAsync<Category>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetForCategoryTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/category/" + _guid.ToString() + "/things");
                var result = await response.Content.ReadAsAsync<IEnumerable<ThingWithLend>>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PostCategoryTest()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "Sample" },
                { "About", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PostAsync("/category", new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<Category>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task PutCategoryTest()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "Sample" },
                { "About", "Sample" }
            };
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.PutAsync("/category/" + _guid.ToString(), new FormUrlEncodedContent(values));
                var result = await response.Content.ReadAsAsync<Category>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteCategoryTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync("/category/" + _guid.ToString());
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteAndReplaceCategoryTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync
                    ("/category/" + _guid.ToString() + "/replace?replacementId=" + _guid.ToString());
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
