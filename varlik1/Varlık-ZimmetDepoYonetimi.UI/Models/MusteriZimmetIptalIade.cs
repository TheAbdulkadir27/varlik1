namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class MusteriZimmetIptalIade
{
    public int MusteriZimmetİptalIadeId { get; set; }

    public int? MusteriZimmetId { get; set; }

    public DateTime? Tarih { get; set; }

    public string? Aciklama { get; set; }

    public bool? IadeMi { get; set; }

    public bool? AktifMi { get; set; }

    public virtual MusteriZimmet? MusteriZimmet { get; set; }
}
