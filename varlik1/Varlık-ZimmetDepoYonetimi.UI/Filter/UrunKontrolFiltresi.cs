using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Varlık_ZimmetDepoYonetimi.UI.Filters
{
    public class UrunKontrolFiltresi : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Ürün kontrol işlemleri burada yapılabilir
            // Örneğin, ürünün geçerli olup olmadığını kontrol edebiliriz.

            var urunId = context.ActionArguments["id"] != null ? (int)context.ActionArguments["id"] : 0; // id parametresi varsa al, yoksa 0 olarak ata
            if (urunId <= 0)
            {
                context.Result = new BadRequestResult();
                return;
            }

            await next();
        }
    }
}