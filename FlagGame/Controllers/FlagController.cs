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
            int currentScore = 0;
            int currentRound = 1;
            ViewBag.CurrentScore = currentScore;
            ViewBag.CurrentRound = currentRound;
            

            flagList = flagMethods.SelectFlags(1, out error);
            fullFlagList = flagMethods.SelectFlags(1, out error);
            int randomFlag = random.Next(flagList.Count);
            int listCount = flagList.Count;
            ViewBag.ListCount = listCount;

            List<String> flagNames = new List<String>();
            flagNames.Add(flagMethods.formatFlagName(flagList[randomFlag].name));
            flagNames.Add(flagMethods.formatFlagName(flagList[random.Next(flagList.Count)].name));
            flagNames.Add(flagMethods.formatFlagName(flagList[random.Next(flagList.Count)].name));
            

            List<String> shuffledList = flagMethods.shuffleStringList(flagNames);
            ViewBag.FlagName1 = shuffledList[0];
            ViewBag.FlagName2 = shuffledList[1];
            ViewBag.FlagName3 = shuffledList[2];
            ViewBag.FlagImage = flagList[randomFlag].imagePath;

            FlagDetails temp = flagList[randomFlag];
            flagList.Remove(temp);

            return View();
        }
    }

}
