using Intex2023.Models;
using Intex2023.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace Intex2023.Controllers
{
    public class HomeController : Controller
    {

        private BurialContext context { get; set; }

        private IBurialRepository repo;

        public HomeController(IBurialRepository temp, BurialContext x)
        {
            repo = temp;
            context = x;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BurialRecords(string burialhaircolor, string sex, string eastwest, string ageatdeath, string northsouth, string headdirection, int pageNum = 1)
        {

            int pageSize = 15;

            var x = new BurialsViewModel
            {
                burialmain = repo.burialmain
                .Where(b => b.haircolor == burialhaircolor || burialhaircolor == null)
                .Where(b => b.sex == sex || sex == null)
                .Where(b => b.eastwest == eastwest || eastwest == null)
                .Where(b => b.ageatdeath == ageatdeath || ageatdeath == null)
                .Where(b => b.northsouth == northsouth || northsouth == null)
                .Where(b => b.headdirection == headdirection || headdirection == null)

                //.OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumRecords =
                        (burialhaircolor == null && sex == null && eastwest == null && ageatdeath == null? repo.burialmain.Count() : repo.burialmain.Where(x => x.haircolor == burialhaircolor).Where(x => x.sex == sex).Where(x => x.eastwest == eastwest).Where(x => x.ageatdeath == ageatdeath).Where(x => x.northsouth == northsouth).Where(x => x.headdirection == headdirection).Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };

            return View(x);
        }


        public IActionResult IndividualBurial(long id)
        {
            //var data = new BurialsViewModel
            //{
            //    burialmain = repo.burialmain
            //    .Where(x => x.id == id)
            //};

            //var data = repo.burialmain.Where(x => x.id == id).Take(1);

            //var data = repo.burialmain.SingleOrDefault(x => x.id == id);

            //var data = repo.burialmain.Where(x => x.id == id).FirstOrDefault();

            var data = context.burialmain.First(x => x.id == id);

            return View(data);
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddRecord()
        {
            ViewBag.burialmain = context.burialmain.ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddRecord(Burial b)
        {
            if (ModelState.IsValid)
            {
                context.Add(b);
                context.SaveChanges();

                return View("Confirmation", b);
            }
            else
            {
                ViewBag.burialmain = context.burialmain.ToList();

                return View(b);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(long ID)
        {
            ViewBag.burialmain = context.burialmain.ToList();
            var individual = context.burialmain.SingleOrDefault(x => x.id == ID);
            return View("EditRecord", individual);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Burial b)
        {
            context.Update(b);
            context.SaveChanges();
            return RedirectToAction("BurialRecords");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(long ID)
        {
            var individual = context.burialmain.SingleOrDefault(x => x.id == ID);
            return View(individual);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Burial ar)
        {
            context.burialmain.Remove(ar);
            context.SaveChanges();
            return RedirectToAction("BurialRecords");
        }

        public int GetNextId()
        {
            using (var context = new BurialContext()) 
            {
                var lastBurial = context.burialmain
                    .OrderByDescending(b => b.id)
                    .FirstOrDefault();

                if (lastBurial != null)
                {
                    return (int)(long)(lastBurial.id + 1);
                }

                return 1;
            }
        }
    }
}