using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GMLS_Part2.Models
{
    public enum ContractStatus
    {
        Draft,
        Active,
        Expired,
        OnHold
    }

    public class Contract
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ContractStatus Status { get; set; }

        public string ServiceLevel { get; set; }

        // Stores uploaded PDF path in database
        public string? SignedAgreementPath { get; set; }

        // Used for file upload only (not stored in database)
        [NotMapped]
        public IFormFile? AgreementFile { get; set; }
    }
}