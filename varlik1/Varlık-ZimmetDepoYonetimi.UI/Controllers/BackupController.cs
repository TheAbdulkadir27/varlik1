using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models.Backup_And_Restore;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class BackupController : Controller
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        public string _connectionString = string.Empty;
        public BackupController()
        {
            _connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!.ToString();
        }

        [Authorize(Policy = Permissions.BackupRestore.Yedek)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = Permissions.BackupRestore.Yedek)]
        public IActionResult Backup()
        {
            string backupPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "backups", $"backup_{DateTime.Now:yyyy/MM/dd_HH/mm/ss}.sql");
            Directory.CreateDirectory(Path.GetDirectoryName(backupPath));
            var list = RestoreAndBackup.GetAllTableNames(_connectionString);
            RestoreAndBackup.BackupTable(_connectionString, list, backupPath);
            TempData["Message"] = "Yedek başarıyla alındı.";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Policy = Permissions.BackupRestore.Yedek)]
        public IActionResult Restore(string fileName)
        {
            string backupPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "backups", fileName);
            var list = RestoreAndBackup.GetAllTableNames(_connectionString);
            RestoreAndBackup.RestoreTable(_connectionString, list, backupPath);
            TempData["Message"] = "Veritabanı başarıyla geri yüklendi.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult BackupFiles()
        {
            string backupFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "backups");
            var files = Directory.Exists(backupFolder)
                ? Directory.GetFiles(backupFolder).Select(Path.GetFileName).ToList()
                : new List<string>();

            return PartialView("_BackupFilesPartial", files);
        }
        public IActionResult Delete(string FileName)
        {
            string backupFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "backups", FileName);
            if (System.IO.File.Exists(backupFolder))
            {
                System.IO.File.Delete(backupFolder);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("Dosya adı belirtilmedi.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Backups", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Dosya bulunamadı.");

            var mimeType = "application/octet-stream";

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, mimeType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAndRestore(IFormFile backupFile)
        {
            if (backupFile == null || backupFile.Length == 0)
                return BadRequest("Dosya seçilmedi.");

            var filePath = Path.Combine("C:\\BackupUploads", backupFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await backupFile.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}
