using System.Net.Http;
using Xunit;

namespace GMLS_Part2.Tests.Integration
{
    public class ContractsApiTests
    {
        [Fact]
        public async Task GetContracts_ReturnsSuccessStatusCode()
        {
            var client = new HttpClient();

            var response =
                await client.GetAsync(
                    "https://localhost:7152/api/Contracts");

            Assert.True(
                response.IsSuccessStatusCode);
        }
    }
}