using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2023.Models;
//Manages the east west filter
namespace Intex2023.Components
{
    public class EastWestViewComponent : ViewComponent
    {

        private IBurialRepository repo { get; set; }
        public EastWestViewComponent(IBurialRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedEastWest = RouteData?.Values["eastwest"];
            var eastwest = repo.burialmain
                .Select(x => x.eastwest)
                .Distinct()
                .OrderBy(x => x);

            return View(eastwest);
        }
    }
}
