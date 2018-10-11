using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace IntegrationTests
{
    public class TestBase
    {
        private TestServer _server;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            Client = _server.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}