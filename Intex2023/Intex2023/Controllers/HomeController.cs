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

        public IActionResult BurialRecords(string burialCategory, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BurialsViewModel
            {
                Burials = repo.Burials
                .Where(b => b.Category == burialCategory || burialCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumRecords =
                        (burialCategory == null ? repo.Burials.Count() : repo.Burials.Where(x => x.Category == burialCategory).Count()),
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