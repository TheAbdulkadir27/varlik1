using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class StokHareket
    {
        [Key]
        public int StokHareketId { get; set; }
        public DateTime Tarih { get; set; }

        [Required]
        public int UrunId { get; set; }

        [ForeignKey("UrunId")]
        public Urun? Urun { get; set; }

        [Required]
        public string? IslemTipi { get; set; }

        public int Miktar { get; set; }

        [Required]
        public string? Kullanici { get; set; }

        public bool? AktifMi { get; set; }
    }
}