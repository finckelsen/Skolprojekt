using FlagGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlagGame.Controllers
{
    public class FlagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Play()
        {
            List<FlagDetails> flagList = new List<FlagDetails>();
            FlagMethods flagMethods = new FlagMethods();
            string error = "";
            Random random = new Random();
            flagList = flagMethods.SelectFlags(1, out error);
            ViewBag.ListSize = flagList.Count;
            int randomFlag = random.Next(flagList.Count);
            //ViewBag.Country = "/lib/images/flags/argentina.png";
            ViewBag.CountryName = flagList[randomFlag].name;
            ViewBag.Country = flagList[randomFlag].imagePath;
            return View();
        }
    }
}
