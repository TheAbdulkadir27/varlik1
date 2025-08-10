using System.ComponentModel.DataAnnotations;

namespace Varlık_ZimmetDepoYonetimi.UI.Models;

public partial class Statu
{
    public int? StatuId { get; set; }

    public string? StatuAdi { get; set; }

    public bool? AktifMi { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public virtual ICollection<UrunStatu> UrunStatus { get; set; } = new List<UrunStatu>();
}
