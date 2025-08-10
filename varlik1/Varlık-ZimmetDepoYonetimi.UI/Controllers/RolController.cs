using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;


namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolController : Controller
    {
        private readonly IRolService _roleService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolController(IRolService roleService, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roller = await _roleManager.Roles.ToListAsync();
            var rolViewModels = new List<RolViewModel>();

            foreach (var rol in roller)
            {
                var kullaniciSayisi = await _userManager.GetUsersInRoleAsync(rol.Name);
                var claims = await _roleManager.GetClaimsAsync(rol);

                rolViewModels.Add(new RolViewModel
                {
                    RolId = rol.Id,
                    RolAdi = rol.Name,
                    KullaniciSayisi = kullaniciSayisi.Count,
                    Yetkiler = claims.Select(c => c.Value).ToList()
                });
            }

            return View(rolViewModels);
        }

        public IActionResult Create()
        {
            var model = new RolEditViewModel
            {
                TumYetkiler = GetTumYetkiler()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var role = new IdentityRole(model.RolAdi);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                foreach (var yetkiId in model.SeciliYetkiler)
                {
                    await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", yetkiId));
                }
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.TumYetkiler = GetTumYetkiler();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var claims = await _roleManager.GetClaimsAsync(role);
            var model = new RolEditViewModel
            {
                RolId = role.Id,
                RolAdi = role.Name,
                TumYetkiler = GetTumYetkiler(),
                SeciliYetkiler = claims.Select(c => c.Value).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RolEditViewModel model)
        {
            if (id != model.RolId) return NotFound();

            if (!ModelState.IsValid)
            {
                model.TumYetkiler = GetTumYetkiler();
                return View(model);
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            role.Name = model.RolAdi;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                // Mevcut yetkileri temizle
                var currentClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in currentClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                // Yeni yetkileri ekle
                foreach (var yetkiId in model.SeciliYetkiler)
                {
                    await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", yetkiId));
                }
                //await signInManager.RefreshSignInAsync(user)
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.TumYetkiler = GetTumYetkiler();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(Index));
        }

        private List<YetkiViewModel> GetTumYetkiler()
        {
            var yetkiler = new List<YetkiViewModel>();

            // Şirket yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Goruntuleme, Ad = "Şirket Görüntüleme", Aciklama = "Şirketleri görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Ekleme, Ad = "Şirket Ekleme", Aciklama = "Yeni şirket ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Duzenleme, Ad = "Şirket Düzenleme", Aciklama = "Şirket bilgilerini düzenleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Silme, Ad = "Şirket Silme", Aciklama = "Şirket silme yetkisi" });


            //ekip yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Goruntuleme, Ad = "Ekip Görüntüleme", Aciklama = "Ekip görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Ekleme, Ad = "Ekip Ekleme", Aciklama = "Ekip ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Duzenleme, Ad = "Ekip Düzenleme", Aciklama = "Ekip bilgilerini düzenleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Silme, Ad = "Ekip Silme", Aciklama = "Ekip silme yetkisi" });


            //calısan yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Goruntuleme, Ad = "Calisan Görüntüleme", Aciklama = "Calisan görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Ekleme, Ad = "Calisan Ekleme", Aciklama = "Calisan ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Duzenleme, Ad = "Calisan Düzenleme", Aciklama = "Calisan bilgilerini düzenleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Silme, Ad = "Calisan Silme", Aciklama = "Calisan silme yetkisi" });


            // Ürün yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Goruntuleme, Ad = "Ürün Görüntüleme", Aciklama = "Ürünleri görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Ekleme, Ad = "Ürün Ekleme", Aciklama = "Yeni ürün ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Duzenleme, Ad = "Ürün Düzenleme", Aciklama = "Ürün bilgilerini düzenleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Silme, Ad = "Ürün Silme", Aciklama = "Ürün silme yetkisi" });

            //model yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Goruntuleme, Ad = "Model Görüntüleme", Aciklama = "Model görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Ekleme, Ad = "Model Ekleme", Aciklama = "Model ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Duzenleme, Ad = "Model Düzenleme", Aciklama = "Model bilgilerini düzenleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Silme, Ad = "Model Silme", Aciklama = "Model silme yetkisi" });

            //statu ve yedek
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Statuler.Goruntuleme, Ad = "Statu Görüntüleme", Aciklama = "Mevcut Statular Görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.BackupRestore.Yedek, Ad = "yedek alma", Aciklama = "veritabanı yedek alma yetkisi" });

            // Zimmet yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Zimmetler.Goruntuleme, Ad = "Zimmet Görüntüleme", Aciklama = "Zimmetleri görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Zimmetler.Atama, Ad = "Zimmet Atama", Aciklama = "Zimmet atama yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Zimmetler.IptalEtme, Ad = "Zimmet İptal", Aciklama = "Zimmet iptal etme yetkisi" });


            //kayıpcalıntı
            yetkiler.Add(new YetkiViewModel { Id = Permissions.KayıpCalıntı.Goruntuleme, Ad = "KayıpCalıntı Görüntüleme", Aciklama = "KayıpCalıntı görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.KayıpCalıntı.Ekleme, Ad = "KayıpCalıntı ekle", Aciklama = "KayıpCalıntı ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.KayıpCalıntı.Silme, Ad = "KayıpCalıntı sil", Aciklama = "KayıpCalıntı silme yetkisi" });

            // Depo yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Goruntuleme, Ad = "Depo Görüntüleme", Aciklama = "Depoları görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Ekleme, Ad = "Depo Ekleme", Aciklama = "Yeni depo ekleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Duzenleme, Ad = "Depo Düzenleme", Aciklama = "Depo bilgilerini düzenleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Silme, Ad = "Depo silme", Aciklama = "Depo silme yetkisi" });

            // Rapor yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Raporlar.ZimmetRaporu, Ad = "Zimmet Raporu", Aciklama = "Zimmet raporlarını görüntüleme yetkisi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Raporlar.StokRaporu, Ad = "Stok Raporu", Aciklama = "Stok raporlarını görüntüleme yetkisi" });

            return yetkiler;
        }
    }
}
