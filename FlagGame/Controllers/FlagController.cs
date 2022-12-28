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

        public IActionResult Play(int flagDifficulty)
        {
            List<FlagDetails> flagList = new List<FlagDetails>();
            List<FlagDetails> fullFlagList = new List<FlagDetails>();
            FlagMethods flagMethods = new FlagMethods();
            string error = "";
            Random random = new Random();
            flagList = flagMethods.SelectFlags(1, out error);
            fullFlagList = flagMethods.SelectFlags(1, out error);
            ViewBag.ListSize = flagList.Count;
            int randomFlag = random.Next(flagList.Count);
            //ViewBag.Country = "/lib/images/flags/argentina.png";
            ViewBag.CorrectFlagName = flagMethods.formatFlagName(flagList[randomFlag].name);
            ViewBag.RandomFlagName1 = flagMethods.formatFlagName(flagList[random.Next(flagList.Count)].name);
            ViewBag.RandomFlagName2 = flagMethods.formatFlagName(flagList[random.Next(flagList.Count)].name);
            ViewBag.FlagImage = flagList[randomFlag].imagePath;
            return View();
        }
    }

}
