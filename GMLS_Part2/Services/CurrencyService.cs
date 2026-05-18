using System.Net.Http.Json;

namespace GMLS_Part2.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // =====================================================
        // CONVERT USD TO ZAR USING API
        // =====================================================

        public async Task<decimal> ConvertUsdToZarAsync(
            decimal usdAmount)
        {
            try
            {
                // =============================================
                // FREE EXCHANGE RATE API
                // =============================================

                var url =
                    "https://open.er-api.com/v6/latest/USD";

                var response =
                    await _httpClient
                        .GetFromJsonAsync<ExchangeRateResponse>(url);

                // =============================================
                // CHECK API RESPONSE
                // =============================================

                if (response != null &&
                    response.Rates.ContainsKey("ZAR"))
                {
                    var zarRate =
                        response.Rates["ZAR"];

                    return CalculateZarAmount(
                        usdAmount,
                        zarRate);
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        // =====================================================
        // CALCULATE ZAR AMOUNT
        // =====================================================

        public decimal CalculateZarAmount(
            decimal usdAmount,
            decimal exchangeRate)
        {
            return usdAmount * exchangeRate;
        }
    }

    // =====================================================
    // API RESPONSE MODEL
    // =====================================================

    public class ExchangeRateResponse
    {
        public Dictionary<string, decimal> Rates
        { get; set; }
            = new Dictionary<string, decimal>();
    }
}