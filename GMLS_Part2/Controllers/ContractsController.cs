using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMLS_Part2.Data;
using GMLS_Part2.Models;
using GMLS_Part2.Services;

namespace GMLS_Part2.Controllers
{
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly FileValidationService _fileValidationService;

        public ContractsController(
            AppDbContext context,
            FileValidationService fileValidationService)
        {
            _context = context;
            _fileValidationService =
                fileValidationService;
        }

        // =====================================================
        // CLIENT DROPDOWN
        // =====================================================

        private SelectList GetClientSelectList(
            int? selectedId = null)
        {
            return new SelectList(
                _context.Clients.Select(c =>
                    new
                    {
                        c.Id,
                        c.Name
                    }),
                "Id",
                "Name",
                selectedId);
        }

        // =====================================================
        // SAVE PDF
        // =====================================================

        private async Task<(bool success,
            string? fileUrl,
            string? error)>
            SaveContractPdfAsync(IFormFile file)
        {
            // =========================================
            // VALIDATE FILE
            // =========================================

            var validationResult =
                _fileValidationService
                    .ValidatePdfFile(file);

            if (!validationResult.IsValid)
            {
                return (
                    false,
                    null,
                    validationResult.ErrorMessage);
            }

            // =========================================
            // UPLOADS FOLDER
            // =========================================

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads");

            Directory.CreateDirectory(
                uploadsFolder);

            // =========================================
            // SAFE FILE NAME
            // =========================================

            var safeFileName =
                Path.GetFileName(file.FileName);

            var fileName =
                $"{Guid.NewGuid()}_{safeFileName}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            // =========================================
            // SAVE FILE
            // =========================================

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await file.CopyToAsync(stream);

            return (
                true,
                "/uploads/" + fileName,
                null);
        }

        // =====================================================
        // DELETE FILE
        // =====================================================

        private void DeleteFileIfExists(
            string? fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return;

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                fileUrl.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        // =====================================================
        // INDEX + FILTER
        // =====================================================

        public async Task<IActionResult> Index(
            ContractStatus? status,
            DateTime? startDate,
            DateTime? endDate)
        {
            var contracts = _context.Contracts
                .Include(c => c.Client)
                .AsQueryable();

            // FILTER STATUS

            if (status.HasValue)
            {
                contracts = contracts
                    .Where(c =>
                        c.Status == status.Value);
            }

            // FILTER START DATE

            if (startDate.HasValue)
            {
                contracts = contracts
                    .Where(c =>
                        c.StartDate >= startDate.Value);
            }

            // FILTER END DATE

            if (endDate.HasValue)
            {
                contracts = contracts
                    .Where(c =>
                        c.EndDate <= endDate.Value);
            }

            return View(
                await contracts.ToListAsync());
        }

        // =====================================================
        // DETAILS
        // =====================================================

        public async Task<IActionResult> Details(
            int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(
                    c => c.Id == id);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        // =====================================================
        // CREATE GET
        // =====================================================

        public IActionResult Create()
        {
            ViewBag.ClientId =
                GetClientSelectList();

            return View();
        }

        // =====================================================
        // CREATE POST
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Contract contract)
        {
            if (ModelState.IsValid)
            {
                // =====================================
                // PDF UPLOAD
                // =====================================

                if (contract.AgreementFile != null &&
                    contract.AgreementFile.Length > 0)
                {
                    var (success,
                        fileUrl,
                        error) =
                        await SaveContractPdfAsync(
                            contract.AgreementFile);

                    if (!success)
                    {
                        ModelState.AddModelError(
                            "",
                            error!);

                        ViewBag.ClientId =
                            GetClientSelectList(
                                contract.ClientId);

                        return View(contract);
                    }

                    contract.SignedAgreementPath =
                        fileUrl;
                }

                _context.Contracts.Add(contract);

                await _context.SaveChangesAsync();

                return RedirectToAction(
                    nameof(Index));
            }

            ViewBag.ClientId =
                GetClientSelectList(
                    contract.ClientId);

            return View(contract);
        }

        // =====================================================
        // EDIT GET
        // =====================================================

        public async Task<IActionResult> Edit(
            int? id)
        {
            if (id == null)
                return NotFound();

            var contract =
                await _context.Contracts
                .FindAsync(id);

            if (contract == null)
                return NotFound();

            ViewBag.ClientId =
                GetClientSelectList(
                    contract.ClientId);

            return View(contract);
        }

        // =====================================================
        // EDIT POST
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Contract contract)
        {
            if (id != contract.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingContract =
                        await _context.Contracts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            c => c.Id == id);

                    if (existingContract == null)
                        return NotFound();

                    // =================================
                    // NEW PDF
                    // =================================

                    if (contract.AgreementFile != null &&
                        contract.AgreementFile.Length > 0)
                    {
                        var (success,
                            fileUrl,
                            error) =
                            await SaveContractPdfAsync(
                                contract.AgreementFile);

                        if (!success)
                        {
                            ModelState.AddModelError(
                                "",
                                error!);

                            ViewBag.ClientId =
                                GetClientSelectList(
                                    contract.ClientId);

                            return View(contract);
                        }

                        // DELETE OLD FILE

                        DeleteFileIfExists(
                            existingContract
                            .SignedAgreementPath);

                        contract.SignedAgreementPath =
                            fileUrl;
                    }
                    else
                    {
                        contract.SignedAgreementPath =
                            existingContract
                            .SignedAgreementPath;
                    }

                    _context.Update(contract);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Contracts.Any(
                        c => c.Id == contract.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(
                    nameof(Index));
            }

            ViewBag.ClientId =
                GetClientSelectList(
                    contract.ClientId);

            return View(contract);
        }

        // =====================================================
        // DELETE GET
        // =====================================================

        public async Task<IActionResult> Delete(
            int? id)
        {
            if (id == null)
                return NotFound();

            var contract =
                await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(
                    c => c.Id == id);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        // =====================================================
        // DELETE POST
        // =====================================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(int id)
        {
            var contract =
                await _context.Contracts
                .FindAsync(id);

            if (contract != null)
            {
                DeleteFileIfExists(
                    contract.SignedAgreementPath);

                _context.Contracts.Remove(contract);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(
                nameof(Index));
        }

        // =====================================================
        // DOWNLOAD PDF
        // =====================================================

        public async Task<IActionResult> Download(
            int id,
            bool inline = false)
        {
            var contract =
                await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(
                    c => c.Id == id);

            if (contract == null ||
                string.IsNullOrEmpty(
                    contract.SignedAgreementPath))
            {
                return NotFound();
            }

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                contract.SignedAgreementPath
                .TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var clientName =
                contract.Client?.Name
                ?.Replace(" ", "_")
                ?? "Unknown";

            var fileName =
                $"Contract_{clientName}_{contract.Id}.pdf";

            var disposition =
                inline ? "inline" : "attachment";

            Response.Headers.Append(
                "Content-Disposition",
                $"{disposition}; filename={fileName}");

            var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read);

            return File(
                stream,
                "application/pdf");
        }
    }
}