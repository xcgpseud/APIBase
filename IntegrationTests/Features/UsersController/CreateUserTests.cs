using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.Models;
using Logic.Helpers;
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
            Assert.AreEqual(requestUser.Email, responseBody.ResponseObject.Email);
            Assert.Greater(responseBody.ResponseObject.UserId, 0);
        }

        [Test]
        public async Task CreateUserStoresUserInDatabase()
        {
            // ARRANGE
            var requestUser = TestHelpers.CreateUserWithRandomData();
            var request = TestHelpers.CreatePostRequest("api/users", requestUser);
            
            // ACT
            var (response, responseBody) = await SendRequest<User>(request);
            Assert.NotNull(responseBody.ResponseObject);
            var dbUser = DatabaseContext.Users.FirstOrDefault(user =>
                user.UserId == responseBody.ResponseObject.UserId);
            
            // ASSERT
            Assert.NotNull(dbUser);
            Assert.AreEqual(requestUser.Username, dbUser.Username);
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify(requestUser.Password, dbUser.Password));
            Assert.AreNotEqual(requestUser.Password, dbUser.Password);
            Assert.AreEqual(requestUser.Email, dbUser.Email);
            Assert.Greater(dbUser.UserId, 0);
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

        [Test]
        public async Task CreateUserEmptyUsernameEmailPasswordReturnsBadRequestWithErrors()
        {
            // ARRANGE
            var requestUser = TestHelpers.CreateUserWithRandomData(
                username: "", password: "", email: "");
            var request = TestHelpers.CreatePostRequest("api/users", requestUser);
            
            // ACT
            var (response, responseBody) = await SendRequest<User>(request);
            var (errorKeys, errorValues) = TestHelpers.GetAllKeysAndValues(responseBody.Errors);
            
            // ASSERT
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Username", errorKeys);
            Assert.Contains("Password", errorKeys);
            Assert.Contains("Email", errorKeys);
            Assert.Contains(UserConstants.MissingUsername, errorValues);
            Assert.Contains(UserConstants.MissingPassword, errorValues);
            Assert.Contains(UserConstants.MissingEmail, errorValues);
        }
        
        [Test]
        public async Task CreateUserNullUsernameEmailPasswordReturnsBadRequestWithErrors()
        {
            // ARRANGE
            var requestUser = TestHelpers.CreateUserWithRandomData(
                useNulls: true);
            var request = TestHelpers.CreatePostRequest("api/users", requestUser);
            
            // ACT
            var (response, responseBody) = await SendRequest<User>(request);
            var (errorKeys, errorValues) = TestHelpers.GetAllKeysAndValues(responseBody.Errors);
            
            // ASSERT
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Username", errorKeys);
            Assert.Contains("Password", errorKeys);
            Assert.Contains("Email", errorKeys);
            Assert.Contains(UserConstants.MissingUsername, errorValues);
            Assert.Contains(UserConstants.MissingPassword, errorValues);
            Assert.Contains(UserConstants.MissingEmail, errorValues);
        }
    }
}