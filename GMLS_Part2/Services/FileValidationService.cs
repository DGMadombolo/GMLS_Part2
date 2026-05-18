using Microsoft.AspNetCore.Http;

namespace GMLS_Part2.Services
{
    public class FileValidationService
    {
        // =====================================================
        // MAX FILE SIZE
        // =====================================================

        private const long MaxFileSize =
            10 * 1024 * 1024; // 10MB

        // =====================================================
        // ALLOWED FILE TYPES
        // =====================================================

        private readonly string[] AllowedExtensions =
        {
            ".pdf"
        };

        // =====================================================
        // VALIDATE PDF FILE
        // =====================================================

        public (bool IsValid, string ErrorMessage)
            ValidatePdfFile(IFormFile? file)
        {
            // =============================================
            // CHECK IF FILE EXISTS
            // =============================================

            if (file == null || file.Length == 0)
            {
                return (
                    false,
                    "Please upload a PDF file.");
            }

            // =============================================
            // CHECK FILE SIZE
            // =============================================

            if (file.Length > MaxFileSize)
            {
                return (
                    false,
                    "File size cannot exceed 10MB.");
            }

            // =============================================
            // CHECK FILE EXTENSION
            // =============================================

            var extension =
                Path.GetExtension(file.FileName)
                .ToLower();

            if (!AllowedExtensions.Contains(extension))
            {
                return (
                    false,
                    "Only PDF files are allowed.");
            }

            // =============================================
            // VALID FILE
            // =============================================

            return (
                true,
                string.Empty);
        }
    }
}