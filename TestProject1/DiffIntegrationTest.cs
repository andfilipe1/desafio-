using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace TestProject1
{

    [TestClass]
    public class DiffIntegrationTest
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl = "https://localhost:7164/v1/diff";

        public DiffIntegrationTest()
        {
            httpClient = new WebApplicationFactory<Program>().CreateClient();
        }

  
        [TestMethod]
        public async Task Step1_GetAll_AtFirst()
        {
            string urlSlug = "/";
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl + urlSlug);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual('[', responseContent[0]);
            Assert.AreEqual(']', responseContent[responseContent.Length - 1]);
        }

        [TestMethod]
        public async Task Step2_Put_CreateLeft()
        {
            string urlSlug = "/1/left";
            var jsonData = new { data = "4pyTIMOgIGxhIG1vZGU=" };
            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + urlSlug, new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task Step3_Put_CreateRight()
        {
            string urlSlug = "/1/right";
            var jsonData = new { data = "4pyTIMOgIGxhIG1vZGU=" };
            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + urlSlug, new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        //[TestMethod]
        //public async Task Step5_GetById_CheckEquals()
        //{
        //    string urlSlug = "/1";
        //    HttpResponseMessage response = await httpClient.GetAsync(baseUrl + urlSlug);
        //    //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //    string responseContent = await response.Content.ReadAsStringAsync();
        //    var expectedJson = new { diffResultType = "Equals" };
        //    Assert.AreEqual(JsonConvert.SerializeObject(expectedJson), responseContent.Trim());
        //}


        [TestMethod]
        public async Task Step4_Put_UpdateRight()
        {
            string urlSlug = "/1/right";
            var jsonData = new { data = "JUUyJTlDJTkzJTIwJUMzJUEwJTIwbGElMjBtb2Rl" };
            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + urlSlug, new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task Step5_GetById_ReadEmpty()
        {
            string urlSlug = "/1";
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl + urlSlug);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
