using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class DepoViewModel
{
    public DepoViewModel()
    {
        Sirketler = new List<SelectListItem>();
    }

    public int DepoId { get; set; }

    [Required(ErrorMessage = "Depo adı zorunludur")]
    [StringLength(100)]
    public string? DepoAdi { get; set; }

    [Required(ErrorMessage = "Şirket seçimi zorunludur")]
    public int SirketId { get; set; }

    public bool AktifMi { get; set; }

    public List<SelectListItem> Sirketler { get; set; }
}