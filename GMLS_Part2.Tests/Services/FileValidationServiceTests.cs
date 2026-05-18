using GMLS_Part2.Services;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GMLS_Part2.Tests.Services
{
    public class FileValidationServiceTests
    {
        private readonly FileValidationService _service;

        public FileValidationServiceTests()
        {
            _service = new FileValidationService();
        }

        // =====================================================
        // TEST INVALID FILE TYPE
        // =====================================================

        [Fact]
        public void ValidatePdfFile_ShouldRejectExeFile()
        {
            // =============================================
            // ARRANGE
            // =============================================

            var fileName = "virus.exe";

            var stream =
                new MemoryStream(new byte[100]);

            IFormFile file =
                new FormFile(
                    stream,
                    0,
                    stream.Length,
                    "Data",
                    fileName);

            // =============================================
            // ACT
            // =============================================

            var result =
                _service.ValidatePdfFile(file);

            // =============================================
            // ASSERT
            // =============================================

            Assert.False(result.IsValid);

            Assert.Equal(
                "Only PDF files are allowed.",
                result.ErrorMessage);
        }

        // =====================================================
        // TEST VALID PDF FILE
        // =====================================================

        [Fact]
        public void ValidatePdfFile_ShouldAcceptPdfFile()
        {
            // =============================================
            // ARRANGE
            // =============================================

            var fileName = "contract.pdf";

            var stream =
                new MemoryStream(new byte[100]);

            IFormFile file =
                new FormFile(
                    stream,
                    0,
                    stream.Length,
                    "Data",
                    fileName);

            // =============================================
            // ACT
            // =============================================

            var result =
                _service.ValidatePdfFile(file);

            // =============================================
            // ASSERT
            // =============================================

            Assert.True(result.IsValid);

            Assert.Equal(
                string.Empty,
                result.ErrorMessage);
        }

        // =====================================================
        // TEST LARGE FILE
        // =====================================================

        [Fact]
        public void ValidatePdfFile_ShouldRejectLargeFile()
        {
            // =============================================
            // ARRANGE
            // =============================================

            var fileName = "largefile.pdf";

            // 11MB FILE

            var stream =
                new MemoryStream(
                    new byte[11 * 1024 * 1024]);

            IFormFile file =
                new FormFile(
                    stream,
                    0,
                    stream.Length,
                    "Data",
                    fileName);

            // =============================================
            // ACT
            // =============================================

            var result =
                _service.ValidatePdfFile(file);

            // =============================================
            // ASSERT
            // =============================================

            Assert.False(result.IsValid);

            Assert.Equal(
                "File size cannot exceed 10MB.",
                result.ErrorMessage);
        }
    }
}