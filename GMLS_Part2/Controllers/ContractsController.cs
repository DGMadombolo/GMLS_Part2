using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMLS_Part2.Data;
using GMLS_Part2.Models;

namespace GMLS_Part2.Controllers
{
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public ContractsController(AppDbContext context)
        {
            _context = context;
        }

        // ========================= HELPERS =========================

        private SelectList GetClientSelectList(int? selectedId = null)
        {
            return new SelectList(
                _context.Clients.Select(c => new { c.Id, c.Name }),
                "Id",
                "Name",
                selectedId
            );
        }

        private async Task<(bool success, string? fileUrl, string? error)>
            SaveContractPdfAsync(IFormFile file)
        {
            if (file.Length > MaxFileSize)
                return (false, null, "File must be under 10MB.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".pdf")
                return (false, null, "Only PDF files are allowed.");

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads");

            Directory.CreateDirectory(uploadsFolder);

            var safeFileName = Path.GetFileName(file.FileName);

            var fileName = $"{Guid.NewGuid()}_{safeFileName}";

            var filePath = Path.Combine(uploadsFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return (true, "/uploads/" + fileName, null);
        }

        private void DeleteFileIfExists(string? fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return;

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                fileUrl.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        // ========================= INDEX =========================

        public async Task<IActionResult> Index()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Client)
                .ToListAsync();

            return View(contracts);
        }

        // ========================= DETAILS =========================

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        // ========================= CREATE =========================

        public IActionResult Create()
        {
            ViewBag.ClientId = GetClientSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Contract contract,
            IFormFile agreementFile)
        {
            if (ModelState.IsValid)
            {
                if (agreementFile != null && agreementFile.Length > 0)
                {
                    var (success, fileUrl, error) =
                        await SaveContractPdfAsync(agreementFile);

                    if (!success)
                    {
                        ModelState.AddModelError("", error!);

                        ViewBag.ClientId =
                            GetClientSelectList(contract.ClientId);

                        return View(contract);
                    }

                    contract.SignedAgreementPath = fileUrl;
                }

                _context.Contracts.Add(contract);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId = GetClientSelectList(contract.ClientId);

            return View(contract);
        }

        // ========================= EDIT =========================

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
                return NotFound();

            ViewBag.ClientId =
                GetClientSelectList(contract.ClientId);

            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Contract contract,
            IFormFile agreementFile)
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
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (existingContract == null)
                        return NotFound();

                    if (agreementFile != null &&
                        agreementFile.Length > 0)
                    {
                        var (success, fileUrl, error) =
                            await SaveContractPdfAsync(agreementFile);

                        if (!success)
                        {
                            ModelState.AddModelError("", error!);

                            ViewBag.ClientId =
                                GetClientSelectList(contract.ClientId);

                            return View(contract);
                        }

                        DeleteFileIfExists(
                            existingContract.SignedAgreementPath);

                        contract.SignedAgreementPath = fileUrl;
                    }
                    else
                    {
                        contract.SignedAgreementPath =
                            existingContract.SignedAgreementPath;
                    }

                    _context.Update(contract);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Contracts.Any(c => c.Id == contract.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId =
                GetClientSelectList(contract.ClientId);

            return View(contract);
        }

        // ========================= DELETE =========================

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
                return NotFound();

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract =
                await _context.Contracts.FindAsync(id);

            if (contract != null)
            {
                DeleteFileIfExists(
                    contract.SignedAgreementPath);

                _context.Contracts.Remove(contract);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ========================= DOWNLOAD =========================

        public async Task<IActionResult> Download(
            int id,
            bool inline = false)
        {
            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null ||
                string.IsNullOrEmpty(contract.SignedAgreementPath))
            {
                return NotFound();
            }

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                contract.SignedAgreementPath.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var clientName =
                contract.Client?.Name?.Replace(" ", "_")
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

            return File(stream, "application/pdf");
        }
    }
}