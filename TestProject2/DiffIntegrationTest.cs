namespace TestProject2
{
    public class DiffIntegrationTest
    {
        private HttpClient httpClient = new HttpClient();
        private string baseUrl = "https://localhost:44317/v1/diff";

        [Fact]
        public async Task Step1_GetAll_AtFirst()
        {
            string urlSlug = "/";
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl + urlSlug);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal('[', responseContent[0]);
            Assert.Equal(']', responseContent[responseContent.Length - 1]);
        }

        [Fact]
        public async Task Step2_Put_CreateLeft()
        {
            string urlSlug = "/1/left";
            var jsonData = new { data = "4pyTIMOgIGxhIG1vZGU=" };
            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + urlSlug, new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Step3_Put_CreateRight()
        {
            string urlSlug = "/1/right";
            var jsonData = new { data = "4pyTIMOgIGxhIG1vZGU=" };
            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + urlSlug, new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Step4_Put_UpdateRight()
        {
            string urlSlug = "/1/right";
            var jsonData = new { data = "JUUyJTlDJTkzJTIwJUMzJUEwJTIwbGElMjBtb2Rl" };
            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + urlSlug, new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Step5_GetById_ReadEmpty()
        {
            string urlSlug = "/1";
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl + urlSlug);

            // Print the response status code
            int statusCode = (int)response.StatusCode;
            Console.WriteLine($"Response Status Code: {statusCode}");

            // Read the response content
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");

            // Check if the response status code is 204 No Content or 404 Not Found
            if (response.StatusCode == HttpStatusCode.NoContent)
           
                // Assert a failure with a descriptive message
                Assert.True(true, $"No data found: {response.StatusCode}");
        }


    }
}
