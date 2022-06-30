using Microsoft.AspNetCore.Mvc;

namespace LobbyMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
