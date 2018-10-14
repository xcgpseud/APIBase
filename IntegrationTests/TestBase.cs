using System;
using System.Net.Http;
using System.Threading.Tasks;
using Database.Context;
using Domain.Models.Interfaces;
using Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests
{
    public class TestBase
    {
        private TestServer _server;
        protected HttpClient Client;
        private readonly Uri _hostUri = new Uri(@"http://localhost:1337/");
        protected MainContext DatabaseContext;
        
        #region SetUpAndTearDown
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>())
            {
                BaseAddress = _hostUri
            };
            Client = _server.CreateClient();
            DatabaseContext = _server.Host.Services.GetService<MainContext>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
            _server.Dispose();
        }

        #endregion

        #region Requests

        protected async Task<(HttpResponseMessage, Response<TModel>)> SendRequest<TModel>(HttpRequestMessage request)
            where TModel : IModel, new()
        {
            var response = await Client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<Response<TModel>>(result);

            return (response, responseBody);
        }

        #endregion
    }
}