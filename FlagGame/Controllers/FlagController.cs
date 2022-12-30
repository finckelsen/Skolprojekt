using FlagGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlagGame.Controllers
{
    public class FlagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult prePlay()
        {

            return View();
        }

        public IActionResult Play()
        {

            int flagDifficulty = 2;
            String tempDiff = HttpContext.Request.Cookies["difficulty"];
            String currentScore = HttpContext.Request.Cookies["currentScore"];
            Console.WriteLine(currentScore);
            try
            {
                flagDifficulty = Int32.Parse(tempDiff);
           
            }
            catch
            {

            }
            List<FlagDetails> flagList = new List<FlagDetails>();
            List<FlagDetails> fullFlagList = new List<FlagDetails>();
            FlagMethods flagMethods = new FlagMethods();
            string error = "";
            Random random = new Random();
            int currentScore = 0;
            
            ViewBag.CurrentScore = currentScore;
            ViewBag.CurrentRound = currentRound;
            

            flagList = flagMethods.SelectFlags(flagDifficulty, out error);
            fullFlagList = flagMethods.SelectFlags(flagDifficulty, out error);
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

        public IActionResult preDiff(int flagDifficulty)
        {
            int temp = 0;
            if (Request.Cookies["difficulty"] == null && Request.Cookies["currentScore"] == null)
            {
                HttpContext.Response.Cookies.Append("difficulty", flagDifficulty.ToString());
                HttpContext.Response.Cookies.Append("currentScore", "0");

            }
            else
            {
                Response.Cookies.Delete("difficulty");
                Response.Cookies.Delete("currentScore");
                HttpContext.Response.Cookies.Append("difficulty", flagDifficulty.ToString());
                HttpContext.Response.Cookies.Append("currentScore", "0");

            }
 
            return View();
        }

     
    }

}
