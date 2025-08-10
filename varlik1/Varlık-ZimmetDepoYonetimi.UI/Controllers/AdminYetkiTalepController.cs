using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminYetkiTalepController : Controller
    {
        private readonly VarlikZimmetEnginContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminYetkiTalepController(VarlikZimmetEnginContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Talepler()
        {
            var talepler = _context.YetkiTalepleri
        .Include(t => t.User)
        .Where(t => t.Durum == TalepDurumu.Beklemede)
        .Select(t => new YetkiTalepAdminViewModel
        {
            TalepId = t.Id,
            KullaniciAdi = t.User.UserName,
            Yetki = t.Yetki,
            Aciklama = t.Aciklama,
            TalepTarihi = t.TalepTarihi,
            Durum = t.Durum
        })
        .ToList();
            return View(talepler);
        }

        [HttpPost]
        public async Task<IActionResult> TalebiGuncelle(int talepId, string islem)
        {
            var talep = await _context.YetkiTalepleri
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == talepId);

            if (talep == null)
                return NotFound();

            switch (islem)
            {
                case "Onayla":
                    // Claim eklemeden önce işle
                    talep.Durum = TalepDurumu.Onaylandi;
                    talep.GuncellenmeTarihi = DateTime.Now;
                    var mevcutClaimler = await _userManager.GetClaimsAsync(talep.User);
                    if (!mevcutClaimler.Any(c => c.Type == "Permission" && c.Value == talep.Yetki))
                    {
                        var claim = new Claim("Permission", talep.Yetki);
                        await _userManager.AddClaimAsync(talep.User, claim);
                    }
                    // Diğer aynı talepleri sil
                    var digerTalepler = await _context.YetkiTalepleri
                        .Where(t => t.UserId == talep.UserId && t.Yetki == talep.Yetki)
                        .ToListAsync();
                    _context.YetkiTalepleri.RemoveRange(digerTalepler);
                    break;
                case "Reddet":
                    talep.Durum = TalepDurumu.Reddedildi;
                    talep.GuncellenmeTarihi = DateTime.Now;

                    var redTalepler = await _context.YetkiTalepleri
                        .Where(t => t.UserId == talep.UserId && t.Yetki == talep.Yetki)
                        .ToListAsync();

                    _context.YetkiTalepleri.RemoveRange(redTalepler);
                    break;

                case "Beklet":
                    talep.Durum = TalepDurumu.Beklemede;
                    talep.GuncellenmeTarihi = DateTime.Now;
                    _context.Update(talep); // Sadece güncelle
                    break;

                default:
                    return BadRequest("Geçersiz işlem.");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Talepler));
        }

    }
}
