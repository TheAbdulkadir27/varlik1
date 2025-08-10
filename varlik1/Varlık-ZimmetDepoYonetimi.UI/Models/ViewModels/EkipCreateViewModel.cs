using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class EkipCreateViewModel
{
    [Required(ErrorMessage = "Ekip adı zorunludur")]
    public string? EkipAdi { get; set; }

    public int SirketId { get; set; }

    public List<SelectListItem>? Sirketler { get; set; }

    public bool AktifMi { get; set; } = true;
}