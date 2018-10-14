using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Domain.Models;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public static class TestHelpers
    {
        #region Requests
        
        public static HttpRequestMessage CreatePostRequest(string url, object body)
        {
            var stringBody = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");
            
            return new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = stringBody
            };
        }

        #endregion

        #region Responses

        public static (List<string>, List<string>) GetAllKeysAndValues(
            Dictionary<string, List<string>> errors)
        {
            // Can contain duplicates
            var keys = errors.Keys.ToList();
            var values = errors.Values.SelectMany(x => x).ToList();

            return (keys, values);
        }

        #endregion

        #region DataGeneration

        public static User CreateUserWithRandomData(
            string username = null, string password = null, string email = null, bool useNulls = false)
        {
            var user = useNulls
                ? new User
                {
                    Username = username,
                    Password = password,
                    Email = email
                }
                : new User
                {
                    Username = username ?? Faker.NameFaker.FirstName(),
                    Password = password ?? Faker.StringFaker.AlphaNumeric(20),
                    Email = email ?? Faker.InternetFaker.Email()
                };

            return user;
        }

        #endregion
    }
}