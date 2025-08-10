namespace Varlık_ZimmetDepoYonetimi.UI.Models
{
    public class Rapor
    {
        public int RaporId { get; set; }
        public string? RaporAdi { get; set; }
        public string? Aciklama { get; set; }
        public DateTime? Tarih { get; set; }
        public bool? AktifMi { get; set; }
    }
}