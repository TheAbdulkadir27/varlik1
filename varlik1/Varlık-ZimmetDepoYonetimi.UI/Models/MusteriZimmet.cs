namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class MusteriZimmet
{
    public int MusteriZimmetId { get; set; }

    public int ZimmetId { get; set; }

    public int MusteriId { get; set; }

    public DateTime? TuketmeTarihi { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Musteri Musteri { get; set; } = null!;

    public virtual ICollection<MusteriZimmetIptalIade> MusteriZimmetIptalIades { get; set; } = new List<MusteriZimmetIptalIade>();

    public virtual Zimmet Zimmet { get; set; } = null!;
}
