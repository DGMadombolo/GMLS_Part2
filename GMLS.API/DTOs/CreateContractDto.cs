using GLMS.API.Models;

namespace GLMS.API.DTOs
{
    public class CreateContractDto
    {
        public int ClientId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ContractStatus Status { get; set; }

        public string ServiceLevel { get; set; } = string.Empty;

        public string? SignedAgreementPath { get; set; }
    }
}