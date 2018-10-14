using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;
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
            // ARRANGE
            var requestUser = TestHelpers.CreateUserWithRandomData();
            var request = TestHelpers.CreatePostRequest("api/users", requestUser);
            
            // ACT
            var (response, responseBody) = await SendRequest<User>(request);

            // ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(requestUser.Username, responseBody.ResponseObject.Username);
            Assert.AreEqual(requestUser.Password, responseBody.ResponseObject.Password);
            Assert.AreEqual(requestUser.Email, responseBody.ResponseObject.Email);
            Assert.Greater(responseBody.ResponseObject.UserId, 0);
        }

        [Test]
        public async Task CreateUserInvalidEmailReturnsBadRequestWithError()
        {
            // ARRANGE
            var requestUser = TestHelpers.CreateUserWithRandomData(
                email: "InvalidEmailAddress");
            var request = TestHelpers.CreatePostRequest("api/users", requestUser);

            // ACT
            var (response, responseBody) = await SendRequest<User>(request);
            var (errorKeys, errorValues) = TestHelpers.GetAllKeysAndValues(responseBody.Errors);
            
            // ASSERT
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Email", errorKeys);
            Assert.Contains(UserConstants.InvalidEmail, errorValues);
        }
    }
}