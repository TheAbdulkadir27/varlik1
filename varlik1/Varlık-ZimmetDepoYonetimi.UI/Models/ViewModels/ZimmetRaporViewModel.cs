namespace Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels
{
    public class ZimmetRaporViewModel
    {
        public ZimmetRaporViewModel()
        {
            AktifZimmetler = new List<Zimmet>();
            DepartmanBazliZimmetler = new Dictionary<string, int>();
        }

        public int ToplamZimmetSayisi { get; set; }
        public decimal ZimmetliUrunOrani { get; set; }
        public decimal calintiUrunOrani { get; set; }
        public IEnumerable<Zimmet> AktifZimmetler { get; set; } = new List<Zimmet>();
        public Dictionary<string, int> DepartmanBazliZimmetler { get; set; } = new Dictionary<string, int>();
    }
}