using Microsoft.AspNetCore.Mvc;

namespace FlagGame.Controllers
{
    public class FlagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
