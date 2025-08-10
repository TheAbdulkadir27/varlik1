namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class SayfaYetki
{
    public int SayfaId { get; set; }

    public int YetkiId { get; set; }

    public int RolId { get; set; }

    public bool? AktifMi { get; set; }

    public virtual Rol Rol { get; set; } = null!;

    public virtual Sayfa Sayfa { get; set; } = null!;

    public virtual Yetki Yetki { get; set; } = null!;
}
