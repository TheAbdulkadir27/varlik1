namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Musteri
{
    public int MusteriId { get; set; }

    public string? AdSoyad { get; set; }

    public bool? AktifMi { get; set; }

    public virtual ICollection<MusteriZimmet> MusteriZimmets { get; set; } = new List<MusteriZimmet>();
}
