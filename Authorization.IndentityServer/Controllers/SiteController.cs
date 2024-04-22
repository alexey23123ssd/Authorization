using Microsoft.AspNetCore.Mvc;

namespace Authorization.IndentityServer.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
