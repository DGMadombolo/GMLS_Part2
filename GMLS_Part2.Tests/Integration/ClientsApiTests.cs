using System.Net.Http;
using Xunit;

namespace GMLS_Part2.Tests.Integration
{
    public class ClientsApiTests
    {
        [Fact]
        public async Task GetClients_ReturnsSuccessStatusCode()
        {
            // API must be running

            var client = new HttpClient();

            var response =
                await client.GetAsync(
                    "https://localhost:7152/api/Clients");

            Assert.True(
                response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetClients_ReturnsJson()
        {
            var client = new HttpClient();

            var response =
                await client.GetAsync(
                    "https://localhost:7152/api/Clients");

            var content =
                await response.Content
                    .ReadAsStringAsync();

            Assert.False(
                string.IsNullOrWhiteSpace(
                    content));
        }
    }
}