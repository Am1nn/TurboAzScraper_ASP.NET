using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAzScraper_ASP.NET.Services;

namespace TurboAzScraper_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {


            //PageLinkGenerator pageLinkGenerator = new PageLinkGenerator();
            //pageLinkGenerator.GeneratePageLink(4,true);
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

       
    }
}
