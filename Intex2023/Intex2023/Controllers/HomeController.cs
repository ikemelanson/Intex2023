using Intex2023.Models;
using Intex2023.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Intex2023.Controllers
{
    public class HomeController : Controller
    {
        private IBurialRepository repo;
        public HomeController(IBurialRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BurialRecords(string burialhaircolor, string sex, int pageNum = 1)
        {
            //var filterOptions = new List<string> { "Option 1", "Option 2", "Option 3" };
            //ViewBag.FilterOptions = filterOptions;

            int pageSize = 15;

            var x = new BurialsViewModel
            {
                burialmain = repo.burialmain
                .Where(b => b.haircolor == burialhaircolor || burialhaircolor == null)
                .Where(b => b.sex == sex || sex == null)

                //.OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumRecords =
                        (burialhaircolor == null && sex == null ? repo.burialmain.Count() : repo.burialmain.Where(x => x.haircolor == burialhaircolor).Where(x => x.sex == sex).Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };

            return View(x);
        }
        public IActionResult IndividualBurial()
        {
            return View();
        }
        public IActionResult Supervised()
        {
            return View();
        }
        public IActionResult Unsupervised()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}