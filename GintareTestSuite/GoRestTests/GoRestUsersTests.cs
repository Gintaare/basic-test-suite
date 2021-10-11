using System.ComponentModel.Design;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;


namespace GintareTestSuite
{
    [TestClass]
    public class GoRestUsersTests
    {
        [TestMethod]
        public void GetAllUsers()
        {
            // Arrange
            var client = new RestClient("https://gorest.co.in/public/v1/");
            var request = new RestRequest("users", Method.GET);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = JsonConvert.DeserializeObject<UsersResponse>(response.Content);
            Assert.AreEqual(20, data.data.Length);
            Assert.IsNotNull(data.data[0].name);
        }

        [TestMethod]
        public void GetSpecificUser()
        {
            // Arrange
            var client = new RestClient("https://gorest.co.in/public/v1/");
            var requestUser = new RestRequest("users", Method.GET);
            var responseUser = client.Execute(requestUser);
            var users = JsonConvert.DeserializeObject<UsersResponse>(responseUser.Content);
            var request = new RestRequest("users/" + users.data[2].id, Method.GET);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            var userInfo = users.data[2].name;
            Assert.AreEqual(userInfo, data.data.name);
            var userGender = users.data[2].gender;
            Assert.AreEqual(userGender, data.data.gender);
        }

        [TestMethod]
        public void UpdateSpecificUser()
        {
            // Arrange
            var client = new RestClient("https://gorest.co.in/public/v1/");
            client.AddDefaultHeader("authorization", "Bearer " + Parameters.ApiToken);

            var request = new RestRequest("users", Method.POST);
            var newEmail = Faker.Internet.Email();
            var newUser = new User()
            {
                email = newEmail,
                name = "Eve Smith",
                gender = "female",
                status = "active",

            };

            var JsonBody = JsonConvert.SerializeObject(newUser);
            request.AddJsonBody(JsonBody);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var createdUser = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            Assert.AreEqual(newEmail, createdUser.data.email);
            Assert.AreEqual("Eve Smith", createdUser.data.name);
            Assert.AreEqual("female", createdUser.data.gender);
        }
    }
}