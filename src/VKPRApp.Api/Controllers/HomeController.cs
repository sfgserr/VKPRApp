using Microsoft.AspNetCore.Mvc;

namespace VKPRApp.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("/Pages/Index.cshtml");
        }

        public IActionResult Docs()
        {
            return View("/Pages/Docs.cshtml");
        }
    }
}
