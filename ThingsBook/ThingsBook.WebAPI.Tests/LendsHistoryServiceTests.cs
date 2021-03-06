﻿using Microsoft.Owin.Testing;
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
    public class LendsHistoryServiceTests
    {
        private readonly Guid _guid = new Guid("12345678123456781234567812345678");

        [Test]
        public async Task AuthTest()
        {
            using (var server = TestServer.Create<TestStartupNoAuth>())
            {
                var response = await server.HttpClient.GetAsync("/history");
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Test]
        public async Task GetLendsTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/history");
                var result = await response.Content.ReadAsAsync<IEnumerable<HistLend>>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task GetLendTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.GetAsync("/history/" + _guid);
                var result = await response.Content.ReadAsAsync<HistLend>();
                Assert.IsNotNull(result);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task DeleteHistoricalLendTest()
        {
            using (var server = TestServer.Create<TestStartup>())
            {
                var response = await server.HttpClient.DeleteAsync("/history/" + _guid);
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
