using Microsoft.AspNetCore.Identity;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;
namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class YetkiTalep
    {
        public int Id { get; set; }
        public string UserId { get; set; }  
        public IdentityUser User { get; set; }
        public string Yetki { get; set; }   // Örn: "Permissions.Urunler.Ekleme"
        public string Aciklama { get; set; }
        public TalepDurumu Durum { get; set; } = TalepDurumu.Beklemede;
        public DateTime TalepTarihi { get; set; } = DateTime.Now;
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
