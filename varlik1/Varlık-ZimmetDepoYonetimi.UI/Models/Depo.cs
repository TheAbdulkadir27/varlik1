namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Depo
{
    public int DepoId { get; set; }

    public string? DepoAdi { get; set; }

    public int? SirketId { get; set; }

    public bool? AktifMi { get; set; }

    public virtual ICollection<DepoUrun> DepoUruns { get; set; } = new List<DepoUrun>();

    public virtual Sirket? Sirket { get; set; }
}
