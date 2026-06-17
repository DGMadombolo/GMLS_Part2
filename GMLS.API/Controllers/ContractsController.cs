using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GLMS.API.Data;
using GLMS.API.Models;
using GLMS.API.DTOs;


namespace GLMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContractsController(AppDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // GET ALL CONTRACTS
        // =====================================================

        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .ToListAsync();

            return Ok(contracts);
        }

        // =====================================================
        // GET CONTRACT BY ID
        // =====================================================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContract(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
                return NotFound();

            return Ok(contract);
        }

        // =====================================================
        // CREATE CONTRACT
        // =====================================================

        [HttpPost]
        public async Task<IActionResult> CreateContract(
            CreateContractDto dto)
        {
            var contract = new Contract
            {
                ClientId = dto.ClientId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                ServiceLevel = dto.ServiceLevel,
                SignedAgreementPath = dto.SignedAgreementPath
            };

            _context.Contracts.Add(contract);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContract),
                new { id = contract.Id },
                contract);
        }

        // =====================================================
        // UPDATE CONTRACT
        // =====================================================

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(
            int id,
            UpdateContractDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var contract =
                await _context.Contracts.FindAsync(id);

            if (contract == null)
                return NotFound();

            contract.ClientId = dto.ClientId;
            contract.StartDate = dto.StartDate;
            contract.EndDate = dto.EndDate;
            contract.Status = dto.Status;
            contract.ServiceLevel = dto.ServiceLevel;
            contract.SignedAgreementPath =
                dto.SignedAgreementPath;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =====================================================
        // DELETE CONTRACT
        // =====================================================

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(
            int id)
        {
            var contract =
                await _context.Contracts.FindAsync(id);

            if (contract == null)
                return NotFound();

            _context.Contracts.Remove(contract);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =====================================================
        // UPDATE CONTRACT STATUS
        // =====================================================

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            UpdateContractStatusDto dto)
        {
            var contract =
                await _context.Contracts.FindAsync(id);

            if (contract == null)
                return NotFound();

            if (!Enum.TryParse<ContractStatus>(
                dto.Status,
                true,
                out var status))
            {
                return BadRequest(
                    "Invalid status.");
            }

            contract.Status = status;

            await _context.SaveChangesAsync();

            return Ok(contract);
        }

        // =====================================================
        // EXISTS
        // =====================================================

        private bool ContractExists(int id)
        {
            return _context.Contracts
                .Any(e => e.Id == id);
        }
    }
}