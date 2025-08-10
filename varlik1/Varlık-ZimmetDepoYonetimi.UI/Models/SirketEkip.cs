namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class SirketEkip
{
    public int SirketId { get; set; }

    public int EkipId { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Ekip Ekip { get; set; } = null!;

    public virtual Sirket Sirket { get; set; } = null!;
}
