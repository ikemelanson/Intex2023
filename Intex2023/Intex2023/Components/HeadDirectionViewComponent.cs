using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2023.Models;
//Manages the category page
namespace Intex2023.Components
{
    public class HeadDirectionViewComponent : ViewComponent
    {

        private IBurialRepository repo { get; set; }
        public HeadDirectionViewComponent(IBurialRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedEstimateStature = RouteData?.Values["headdirection"];
            var headdirection = repo.burialmain
                .Select(x => x.headdirection)
                .Distinct()
                .OrderBy(x => x);

            return View(headdirection);
        }
    }
}
