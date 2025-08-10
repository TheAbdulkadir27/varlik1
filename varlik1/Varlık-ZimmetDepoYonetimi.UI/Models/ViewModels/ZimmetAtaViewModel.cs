namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class ZimmetAtaViewModel
    {
        public Zimmet Zimmet { get; set; }
        //public int AtananCalisanId { get; set; }
        //public DateTime ZimmetBaslangicTarihi { get; set; }
        public string Aciklama { get; set; }

        public IEnumerable<Calisan> Calisanlar { get; set; }
    }
}
