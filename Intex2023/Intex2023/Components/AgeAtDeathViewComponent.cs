using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2023.Models;
//Manages the age at death filter
namespace Intex2023.Components
{
    public class AgeAtDeathViewComponent : ViewComponent
    {

        private IBurialRepository repo { get; set; }
        public AgeAtDeathViewComponent(IBurialRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedAgeAtDeath = RouteData?.Values["ageatdeath"];
            var ageatdeath = repo.burialmain
                .Select(x => x.ageatdeath)
                .Distinct()
                .OrderBy(x => x);

            return View(ageatdeath);
        }
    }
}
