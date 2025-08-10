using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();

            var model = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CurrentRoles = userRoles.ToList(),
                AllRoles = allRoles.Select(r => r.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (model.SelectedRoles != null && model.SelectedRoles.Any())
            {
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            bool hasRelations = await HasAnyRelations(user.Id);
            if (hasRelations)
            {
                ModelState.AddModelError("", "Bu kullanıcının Üstüne kayıtlı Zimmet olduğu için silinemez.");
                return View(user);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcı silinirken bir hata oluştu.");
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> KullaniciYetkileri(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var sahipOlduguYetkiler = userClaims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToHashSet();

            // Tüm sistem yetkilerinden sadece kullanıcının sahip olduklarını filtrele
            var tumYetkiler = GetTumYetkiler()
                .Where(y => sahipOlduguYetkiler.Contains(y.Id))
                .ToList();

            var viewModel = new KullaniciYetkiViewModel
            {
                UserId = id,
                Yetkiler = tumYetkiler.Select(y => new YetkiCheckbox
                {
                    Value = y.Id,
                    Aciklama = y.Aciklama,
                    Secili = true // çünkü bu liste zaten sadece sahip olunanlardan oluşuyor
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> KullaniciYetkileri(KullaniciYetkiViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var mevcutClaims = await _userManager.GetClaimsAsync(user);
            var permissionClaims = mevcutClaims.Where(c => c.Type == "Permission").ToList();
            var mevcutYetkiler = permissionClaims.Select(c => c.Value).ToHashSet();

            var seciliYetkiler = model.Yetkiler.Where(y => y.Secili).Select(y => y.Value).ToHashSet();

            // Silinecek yetkiler
            foreach (var claim in permissionClaims)
            {
                if (!seciliYetkiler.Contains(claim.Value))
                {
                    await _userManager.RemoveClaimAsync(user, claim);
                }
            }

            // Eklenecek yeni yetkiler
            foreach (var yetki in seciliYetkiler)
            {
                if (!mevcutYetkiler.Contains(yetki))
                {
                    await _userManager.AddClaimAsync(user, new Claim("Permission", yetki));
                }
            }
            return RedirectToAction(nameof(Index)); // listeye dönüş
        }
        private List<YetkiViewModel> GetTumYetkiler()
        {
            var yetkiler = new List<YetkiViewModel>();

            // Şirket yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Goruntuleme, Ad = "Şirket Görüntüleme", Aciklama = "Şirketlerle İlgili Verileri Görüntülemenize Olanak Sağlar" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Ekleme, Ad = "Şirket Ekleme", Aciklama = "Yeni Şirket Eklemenize Olanak Sağlar" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Duzenleme, Ad = "Şirket Düzenleme", Aciklama = "Şirketlerle İlgili Bilgileri Güncelleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Sirketler.Silme, Ad = "Şirket Silme", Aciklama = "ilgili bir şirketi silebilirsiniz" });


            //ekip yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Goruntuleme, Ad = "Ekip Görüntüleme", Aciklama = "Hangi Şirkette Ne Kadar Ekip Olduğunu Görüntüleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Ekleme, Ad = "Ekip Ekleme", Aciklama = "Yeni Bir Ekip ekleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Duzenleme, Ad = "Ekip Düzenleme", Aciklama = "Ekip Bilgilerinizi Düzenleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Ekipler.Silme, Ad = "Ekip Silme", Aciklama = "Herhangi Bir Ekibi Silebilirsiniz" });


            //calısan yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Goruntuleme, Ad = "Calisan Görüntüleme", Aciklama = "Çalışanlarınızı Görüntüleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Ekleme, Ad = "Calisan Ekleme", Aciklama = "Yeni Çalışan Ekleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Duzenleme, Ad = "Calisan Düzenleme", Aciklama = "Çalışan Bilgilerinizi Düzenleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Calisanlar.Silme, Ad = "Calisan Silme", Aciklama = "Herhangi bir çalışanı silebilirsiniz" });


            // Ürün yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Goruntuleme, Ad = "Ürün Görüntüleme", Aciklama = "Ürünlerinizi Görüntüleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Ekleme, Ad = "Ürün Ekleme", Aciklama = "Yeni Ürün Girişi Yapabilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Duzenleme, Ad = "Ürün Düzenleme", Aciklama = "Ürün Bilgilerinizi Düzenleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Urunler.Silme, Ad = "Ürün Silme", Aciklama = "Ürünlerinizi Silebilirsiniz" });

            //model yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Goruntuleme, Ad = "Model Görüntüleme", Aciklama = "Modelleri Görüntüleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Ekleme, Ad = "Model Ekleme", Aciklama = "Yeni Bir Model Ekleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Duzenleme, Ad = "Model Düzenleme", Aciklama = "Model Bilgilerinizi Düzenleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Modeller.Silme, Ad = "Model Silme", Aciklama = "ilgili modeli silebilirsiniz" });

            //statu ve yedek
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Statuler.Goruntuleme, Ad = "Statu Görüntüleme", Aciklama = "ürünlerin mevcut statularını görüntüleyebilirsiniz(arızalı-kullanımda) gibi" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.BackupRestore.Yedek, Ad = "yedek alma", Aciklama = "Veritabanı Yedek Alma Geri Yükleme veya Veritabanı indirme olarak seçeneğimiz mevcut" });

            // Zimmet yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Zimmetler.Goruntuleme, Ad = "Zimmet Görüntüleme", Aciklama = "Zimmetleri Görüntüleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Zimmetler.Atama, Ad = "Zimmet Atama", Aciklama = "Bir Zimmeti Başka Çalışana Atayabilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Zimmetler.IptalEtme, Ad = "Zimmet İptal", Aciklama = "Herhangi Bir Zimmeti iade Alabilirsiniz" });


            //kayıpcalıntı
            yetkiler.Add(new YetkiViewModel { Id = Permissions.KayıpCalıntı.Goruntuleme, Ad = "KayıpCalıntı Görüntüleme", Aciklama = "Hangi Zimmetli Ürünün Çalındığını Görebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.KayıpCalıntı.Ekleme, Ad = "KayıpCalıntı ekle", Aciklama = "Kayıp veya çalıntı ekleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.KayıpCalıntı.Silme, Ad = "KayıpCalıntı sil", Aciklama = "kayıp çalıntıları silebilirsiniz" });

            // Depo yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Goruntuleme, Ad = "Depo Görüntüleme", Aciklama = "Depoları Görüntüleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Ekleme, Ad = "Depo Ekleme", Aciklama = "Yeni Bir Depo Ekleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Duzenleme, Ad = "Depo Düzenleme", Aciklama = "Depo Bilgilerini Düzenleyebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Depolar.Silme, Ad = "Depo silme", Aciklama = "Mevcut Depoları silebilirsiniz" });

            // Rapor yetkileri
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Raporlar.ZimmetRaporu, Ad = "Zimmet Raporu", Aciklama = "Zimmet Raporlarını Departman,Çalışan Bazında Görebilirsiniz" });
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Raporlar.StokRaporu, Ad = "Stok Raporu", Aciklama = "Stok Raporlarını Görüntüleyebilirsiniz Hangi Depoda Kaç Tane Stoğunuz Bulunduğunu Görebilirsiniz" });
            return yetkiler;
        }
        private async Task<bool> HasAnyRelations(string userId)
        {
            using (var _context = new VarlikZimmetEnginContext())
            {
                int id = _context.Calisan.Where(x=>x.KullaniciAdi ==  userId).FirstOrDefault().CalisanId;
                bool hasLogs = await _context.Zimmet.AnyAsync(l => l.AtananCalisanId == id || l.AtayanCalisanId == id);
                return hasLogs;
            }
        }
       
    }
}