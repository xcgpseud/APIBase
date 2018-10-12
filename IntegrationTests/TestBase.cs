using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests
{
    public class TestBase
    {
        private TestServer _server;
        protected HttpClient Client;
        private readonly Uri _hostUri = new Uri(@"http://localhost:1337/");

        #region SetUpAndTearDown
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>())
            {
                BaseAddress = _hostUri
            };
            Client = _server.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
            _server.Dispose();
        }

        #endregion

        #region Requests

        protected async Task<(HttpResponseMessage, T)> SendRequest<T>(HttpRequestMessage request)
        {
            var response = await Client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<T>(result);

            return (response, responseObject);
        }

        #endregion
    }
}