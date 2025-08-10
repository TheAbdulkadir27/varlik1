using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

public class KullaniciKontrolFiltresi : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate sonrakiAdim)
    {
        // UserManager'ı servislerden al
        var kullaniciYoneticisi = context.HttpContext.RequestServices.GetService<UserManager<IdentityUser>>();

        // Gelen modeli al
        var model = context.ActionArguments["model"] as RegisterViewModel;

        if (model != null)
        {
            // Kullanıcı adı kontrolü
            var mevcutKullaniciAdi = await kullaniciYoneticisi.FindByNameAsync(model.KullaniciAdi);
            if (mevcutKullaniciAdi != null)
            {
                context.ModelState.AddModelError(nameof(model.KullaniciAdi), "Bu kullanıcı adı zaten kayıtlı!");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            // Email kontrolü
            var mevcutEmail = await kullaniciYoneticisi.FindByEmailAsync(model.Email);
            if (mevcutEmail != null)
            {
                context.ModelState.AddModelError(nameof(model.Email), "Bu email zaten kayıtlı!");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }

        await sonrakiAdim();
    }
}