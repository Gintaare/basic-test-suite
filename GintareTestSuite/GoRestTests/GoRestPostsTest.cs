using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace GintareTestSuite
{
    [TestClass]
    public class GoRestPostsTest
    {
        [TestMethod]
        public void GetAllPosts()
        {
            // Arrange
            var client = new RestClient("https://gorest.co.in/public/v1/");
            var request = new RestRequest("posts", Method.GET);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var data = JsonConvert.DeserializeObject<PostsResponse>(response.Content);
            Assert.AreEqual(20, data.data.Length);
        }

        [TestMethod]
        public void GetSpecificPost()
        {
            // Arrange
            var client = new RestClient("https://gorest.co.in/public/v1/");
            var requestPost = new RestRequest("posts", Method.GET);
            var responsePost = client.Execute(requestPost);
            var posts = JsonConvert.DeserializeObject<UsersResponse>(responsePost.Content);
            var request = new RestRequest("posts/" + posts.data[2].id, Method.GET);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var data = JsonConvert.DeserializeObject<PostResponse>(response.Content);
            Assert.AreEqual(1362, data.data.id);
            Assert.AreEqual(667, data.data.user_id);
        }


        [TestMethod]
        public void PostAPost()
        {
            // Arrange
            var client = new RestClient("https://gorest.co.in/public/v1/");
            client.AddDefaultHeader("authorization", "Bearer " + Parameters.ApiToken);

            var request = new RestRequest("posts", Method.POST);
            var newPost = new Post()
            {
                user_id = 14,
                title = "Basic title",
                body = "Color accedo titulus. Ta… Vae derideo accommodo",
            };

            var JsonBody = JsonConvert.SerializeObject(newPost);
            request.AddJsonBody(JsonBody);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, response.Content);
            var createdPost = JsonConvert.DeserializeObject<PostResponse>(response.Content);

            Assert.AreEqual("Basic title", createdPost.data.title);
            Assert.AreEqual(14, createdPost.data.user_id);
        }
    }
}