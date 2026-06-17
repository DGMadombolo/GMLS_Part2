using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GLMS.API.Models
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

        // ================= CLIENT RELATIONSHIP =================

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        // ================= CONTRACT DATES =================

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        // ================= STATUS =================

        [Required]
        public ContractStatus Status { get; set; }

        // ================= SERVICE LEVEL =================

        [Required]
        [Display(Name = "Service Level")]
        public string ServiceLevel { get; set; } = string.Empty;

        // ================= PDF STORAGE =================

        // Stores PDF path in database
        public string? SignedAgreementPath { get; set; }

        // Used ONLY for uploading PDF files
        [NotMapped]
        [Display(Name = "Agreement PDF")]
        public IFormFile? AgreementFile { get; set; }
    }
}