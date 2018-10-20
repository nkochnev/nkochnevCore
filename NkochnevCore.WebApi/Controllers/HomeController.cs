using Microsoft.AspNetCore.Mvc;

namespace NkochnevCore.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return File("index.html", "text/html");
        }
    }
}