using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;
using Varlık_ZimmetDepoYonetimi.UI.Models.ViewModels;

namespace Varlık_ZimmetDepoYonetimi.UI.Controllers
{
    [Authorize]
    public class DövizController : Controller
    {
        public IActionResult Index()
        {
            string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            XDocument xml = XDocument.Load(url);
            var dovizler = xml.Descendants("Currency")
                       .Select(x => new DovizViewModel
                       {
                           Kod = (string)x.Attribute("Kod"),
                           Isim = (string)x.Element("Isim"),
                           ForexBuying = decimal.TryParse((string)x.Element("ForexBuying"),
                         NumberStyles.Any, CultureInfo.InvariantCulture, out var buying) ? buying : 0,
                           ForexSelling = decimal.TryParse((string)x.Element("ForexSelling"),
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var selling) ? selling : 0
                       }).ToList();
            return View(dovizler);
        }
    }
}
