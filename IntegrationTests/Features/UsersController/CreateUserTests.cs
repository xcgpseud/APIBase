using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests.Features.UsersController
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        [Test]
        public async Task CreateUserReturns200WithUserModel()
        {
            var requestBody = new User
            {
                Username = "TestUsername",
                Password = "TestPassword",
                Email = "email@email.com"
            };

            var jsonBody = JsonConvert.SerializeObject(requestBody);
            var stringBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/users")
            {
                Content = stringBody
            };

            var response = await Client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var resultUser = JsonConvert.DeserializeObject<User>(result);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("TestUsername", resultUser.Username);
            Assert.Greater(resultUser.UserId, 0);
        }
    }
}