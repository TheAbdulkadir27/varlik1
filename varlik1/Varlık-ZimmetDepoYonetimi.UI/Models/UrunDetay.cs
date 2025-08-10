namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class UrunDetay
{
    public int UrunDetayId { get; set; }

    public int? UrunId { get; set; }

    public short? Miktar { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Urun? Urun { get; set; }
}
