using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Varlık_ZimmetDepoYonetimi.UI.Models;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class YetkiTalepController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly VarlikZimmetEnginContext _context;
        public YetkiTalepController(UserManager<IdentityUser> userManager, VarlikZimmetEnginContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
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
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Raporlar.ZimmetRaporu, Ad = "Zimmet Raporu", Aciklama = "Zimmet Raporlarını Departman,Çalışan Bazında Görebilirsiniz"});
            yetkiler.Add(new YetkiViewModel { Id = Permissions.Raporlar.StokRaporu, Ad = "Stok Raporu", Aciklama = "Stok Raporlarını Görüntüleyebilirsiniz Hangi Depoda Kaç Tane Stoğunuz Bulunduğunu Görebilirsiniz" });


            // Kullanıcıyı al
            var user = await _userManager.GetUserAsync(User);

            // Kullanıcının sahip olduğu claim'leri al
            var userClaims = await _userManager.GetClaimsAsync(user!);

            // Kullanıcının sahip olduğu "Permission" claim'lerini al
            var sahipOlduguYetkiler = userClaims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            // Sahip olunan yetkileri listeden çıkar
            var filtrelenmisYetkiler = yetkiler
                .Where(y => !sahipOlduguYetkiler.Contains(y.Id))
                .ToList();
            var viewModel = new YetkiTalepViewModel
            {
                TumYetkiler = filtrelenmisYetkiler
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> TalepEt(YetkiTalepViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (model.SecilenYetkiler != null && model.SecilenYetkiler.Any())
            {
                foreach (var yetkiId in model.SecilenYetkiler)
                {
                    var talep = new YetkiTalep
                    {
                        UserId = user!.Id,
                        Yetki = yetkiId,
                        Aciklama = model.Aciklama,
                        Durum = TalepDurumu.Beklemede,
                        TalepTarihi = DateTime.Now
                    };
                   _context.YetkiTalepleri.Add(talep);
                }
                await _context.SaveChangesAsync();
            }
            TempData["message"] = "Talebiniz İletildi";
            return RedirectToAction(nameof(Index));
        }
    }
}
