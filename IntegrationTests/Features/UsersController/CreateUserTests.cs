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
            // Arrange
            var requestUser = TestHelpers.CreateUserWithRandomData();
            var request = TestHelpers.CreatePostRequest("api/users", requestUser);
            
            // ACT
            var (response, responseObject) = await SendRequest<User>(request);

            // ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(requestUser.Username, responseObject.Username);
            Assert.AreEqual(requestUser.Password, responseObject.Password);
            Assert.AreEqual(requestUser.Email, responseObject.Email);
            Assert.Greater(responseObject.UserId, 0);
        }
    }
}