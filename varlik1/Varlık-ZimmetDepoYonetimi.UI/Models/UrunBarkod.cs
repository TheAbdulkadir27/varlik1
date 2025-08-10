namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class UrunBarkod
{
    public int UrunBarkodId { get; set; }

    public Guid? BarkodNo { get; set; }

    public int? UrunId { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Urun? Urun { get; set; }
}
