namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class UrunStatu
{
    public int UrunStatuId { get; set; }

    public int UrunId { get; set; }

    public int StatuId { get; set; }

    public DateTime? Tarih { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Statu Statu { get; set; } = null!;

    public virtual Urun Urun { get; set; } = null!;
}
