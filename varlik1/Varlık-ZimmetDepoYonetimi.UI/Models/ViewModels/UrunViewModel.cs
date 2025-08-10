using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class UrunViewModel
    {
        public UrunViewModel()
        {
            Modeller = new List<SelectListItem>();
            Depolar = new List<SelectListItem>();
            Statuler = new List<SelectListItem>();
            ÜrünGrubu = new List<SelectListItem>();
            ÖlçüBirimi = new List<SelectListItem>();
        }

        public int UrunId { get; set; }

        public int SeriNo { get; set; }
        public string? Number { get; set; }
        public int? ModelId { get; set; }
        public bool? GarantiliMi { get; set; }
        public double? UrunMaliyeti { get; set; }
        public string? Aciklama { get; set; }
        public bool? BarkodluMu { get; set; }
        public double? UrunGuncelFiyat { get; set; }

        [Display(Name = "Statü")]
        public int? StatuId { get; set; }

        public int StokMiktari { get; set; }
        public int? DepoId { get; set; }

        public int? UnitMeasureId { get; set; }
        public int? ProductGroupId { get; set; }
        public List<SelectListItem> Modeller { get; set; }
        public List<SelectListItem> Depolar { get; set; }
        public List<SelectListItem> ÖlçüBirimi { get; set; }
        public List<SelectListItem> ÜrünGrubu { get; set; }
        public List<SelectListItem> Statuler { get; set; } = new List<SelectListItem>();
        public Model? Model { get; set; }
        public Depo? Depo { get; set; }
        public ProductGroup? ProductGroup { get; set; }
        public UnitMeasure? UnitMeasure { get; set; }
        public bool AktifMi { get; set; }
        public Statu? Statu { get; set; }
    }
}