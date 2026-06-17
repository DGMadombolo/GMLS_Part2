using System.Net.Http;
using Xunit;

namespace GMLS_Part2.Tests.Integration
{
    public class ServiceRequestsApiTests
    {
        [Fact]
        public async Task GetServiceRequests_ReturnsSuccessStatusCode()
        {
            var client = new HttpClient();

            var response =
                await client.GetAsync(
                    "https://localhost:7152/api/ServiceRequests");

            Assert.True(
                response.IsSuccessStatusCode);
        }
    }
}