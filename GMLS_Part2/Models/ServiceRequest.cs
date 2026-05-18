using System.ComponentModel.DataAnnotations;

namespace GMLS_Part2.Models
{
    public enum RequestStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public class ServiceRequest
    {
        public int Id { get; set; }

        // ================= CONTRACT =================

        [Required]
        [Display(Name = "Contract")]
        public int ContractId { get; set; }

        public Contract? Contract { get; set; }

        // ================= DESCRIPTION =================

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // ================= COSTS =================

        [Required]
        [Display(Name = "Cost (USD)")]
        public decimal CostUSD { get; set; }

        [Required]
        [Display(Name = "Cost (ZAR)")]
        public decimal CostZAR { get; set; }

        // ================= STATUS =================

        [Required]
        public RequestStatus Status { get; set; }
    }
}