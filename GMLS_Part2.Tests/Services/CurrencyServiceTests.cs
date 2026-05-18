using GMLS_Part2.Services;
using Xunit;

namespace GMLS_Part2.Tests.Services
{
    public class CurrencyServiceTests
    {
        // =====================================================
        // TEST USD TO ZAR CALCULATION
        // =====================================================

        [Fact]
        public void CalculateZarAmount_ShouldConvertCorrectly()
        {
            // =============================================
            // ARRANGE
            // =============================================

            var service =
                new CurrencyService(
                    new HttpClient());

            decimal usdAmount = 100;

            decimal exchangeRate = 18.50m;

            // =============================================
            // ACT
            // =============================================

            var result =
                service.CalculateZarAmount(
                    usdAmount,
                    exchangeRate);

            // =============================================
            // ASSERT
            // =============================================

            Assert.Equal(
                1850,
                result);
        }

        // =====================================================
        // TEST ZERO USD
        // =====================================================

        [Fact]
        public void CalculateZarAmount_ShouldReturnZero()
        {
            // =============================================
            // ARRANGE
            // =============================================

            var service =
                new CurrencyService(
                    new HttpClient());

            decimal usdAmount = 0;

            decimal exchangeRate = 18.50m;

            // =============================================
            // ACT
            // =============================================

            var result =
                service.CalculateZarAmount(
                    usdAmount,
                    exchangeRate);

            // =============================================
            // ASSERT
            // =============================================

            Assert.Equal(
                0,
                result);
        }

        // =====================================================
        // TEST DECIMAL CALCULATIONS
        // =====================================================

        [Fact]
        public void CalculateZarAmount_ShouldHandleDecimals()
        {
            // =============================================
            // ARRANGE
            // =============================================

            var service =
                new CurrencyService(
                    new HttpClient());

            decimal usdAmount = 25.5m;

            decimal exchangeRate = 18.25m;

            // =============================================
            // ACT
            // =============================================

            var result =
                service.CalculateZarAmount(
                    usdAmount,
                    exchangeRate);

            // =============================================
            // ASSERT
            // =============================================

            Assert.Equal(
                465.375m,
                result);
        }
    }
}