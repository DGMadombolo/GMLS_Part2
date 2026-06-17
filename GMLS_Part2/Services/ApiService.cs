using System.Net.Http.Json;
using GMLS_Part2.Models;

namespace GMLS_Part2.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;


    private const string ClientsUrl =
        "https://localhost:7152/api/Clients";

        private const string ContractsUrl =
            "https://localhost:7152/api/Contracts";

        private const string ServiceRequestsUrl =
            "https://localhost:7152/api/ServiceRequests";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // =====================================================
        // CLIENTS
        // =====================================================

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<Client>>(ClientsUrl)
                ?? new List<Client>();
        }

        public async Task<Client?> GetClientAsync(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<Client>(
                    $"{ClientsUrl}/{id}");
        }

        public async Task CreateClientAsync(Client client)
        {
            await _httpClient.PostAsJsonAsync(
                ClientsUrl,
                client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _httpClient.PutAsJsonAsync(
                $"{ClientsUrl}/{client.Id}",
                client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _httpClient.DeleteAsync(
                $"{ClientsUrl}/{id}");
        }

        // =====================================================
        // CONTRACTS
        // =====================================================

        public async Task<List<Contract>> GetContractsAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<Contract>>(
                    ContractsUrl)
                ?? new List<Contract>();
        }

        public async Task<Contract?> GetContractAsync(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<Contract>(
                    $"{ContractsUrl}/{id}");
        }

        public async Task CreateContractAsync(
            Contract contract)
        {
            var payload = new
            {
                contract.ClientId,
                contract.StartDate,
                contract.EndDate,
                contract.Status,
                contract.ServiceLevel,
                contract.SignedAgreementPath
            };

            var response =
                await _httpClient.PostAsJsonAsync(
                    ContractsUrl,
                    payload);

            var errorContent =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Status: {response.StatusCode}\n\n{errorContent}");
            }
        }

        public async Task UpdateContractAsync(
            Contract contract)
        {
            var payload = new
            {
                contract.Id,
                contract.ClientId,
                contract.StartDate,
                contract.EndDate,
                contract.Status,
                contract.ServiceLevel,
                contract.SignedAgreementPath
            };

            var response =
                await _httpClient.PutAsJsonAsync(
                    $"{ContractsUrl}/{contract.Id}",
                    payload);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteContractAsync(int id)
        {
            await _httpClient.DeleteAsync(
                $"{ContractsUrl}/{id}");
        }

        // =====================================================
        // CONTRACT STATUS
        // =====================================================

        public async Task UpdateContractStatusAsync(
            int id,
            string status)
        {
            await _httpClient.PatchAsJsonAsync(
                $"{ContractsUrl}/{id}/status",
                new { status });
        }

        // =====================================================
        // SERVICE REQUESTS
        // =====================================================

        public async Task<List<ServiceRequest>>
            GetServiceRequestsAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<ServiceRequest>>(
                    ServiceRequestsUrl)
                ?? new List<ServiceRequest>();
        }

        public async Task<ServiceRequest?>
            GetServiceRequestAsync(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<ServiceRequest>(
                    $"{ServiceRequestsUrl}/{id}");
        }

        public async Task CreateServiceRequestAsync(
            ServiceRequest request)
        {
            var payload = new
            {
                request.ContractId,
                request.Description,
                request.CostUSD,
                request.CostZAR,
                request.Status
            };

            var response =
                await _httpClient.PostAsJsonAsync(
                    ServiceRequestsUrl,
                    payload);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateServiceRequestAsync(
            ServiceRequest request)
        {
            var payload = new
            {
                request.Id,
                request.ContractId,
                request.Description,
                request.CostUSD,
                request.CostZAR,
                request.Status
            };

            var response =
                await _httpClient.PutAsJsonAsync(
                    $"{ServiceRequestsUrl}/{request.Id}",
                    payload);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteServiceRequestAsync(
            int id)
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"{ServiceRequestsUrl}/{id}");

            response.EnsureSuccessStatusCode();
        }
    }


}
