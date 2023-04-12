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

        public IActionResult BurialRecords(string burialhaircolor, int pageNum = 1)
        {
            int pageSize = 15;

            var x = new BurialsViewModel
            {
                burialmain = repo.burialmain
                .Where(b => b.haircolor == burialhaircolor || burialhaircolor == null)
                //.OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumRecords =
                        (burialhaircolor == null ? repo.burialmain.Count() : repo.burialmain.Where(x => x.haircolor == burialhaircolor).Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };

            return View(x);
        }
        public IActionResult Supervised()
        {
            return View();
        }
        public IActionResult Unsupervised()
        {
            return View();
        }
        public IActionResult Tech()
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