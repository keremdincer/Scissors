using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Scissors.Services;
using Scissors.Dtos;

namespace Scissors.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var url = Request.Url.PathAndQuery.Substring(1);

            if (string.IsNullOrEmpty(url))
            {
                // Not Found'a yönlendir.
                return View("NotFound");
            }


            var shortUrl = ShortUrlService.FindByUrl(url);
            if (shortUrl == null)
            {
                // Not Found'a yönlendir.
                return View("NotFound");
            }

            // ShortUrl için log oluştur.
            // Bağlanan cihazla ilgili bilgileri kaydet.
            var usageDto = new ShortUrlUsageDto() {
                ShortUrlId = shortUrl.Id,
                ClientIP = Request.UserHostAddress,
                ClientBrowser = Request.Browser.Browser,
                ClientDevice = Request.Browser.IsMobileDevice ? "Mobile" : "Desktop",
                ClientOS = Request.Browser.Platform
            };

            try
            {
                ShortUrlService.AddUsage(usageDto);
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR: On adding usage info.");
            }

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}
