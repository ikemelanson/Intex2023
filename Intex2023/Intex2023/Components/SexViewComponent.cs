using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2023.Models;
//Manages the category page
namespace Intex2023.Components
{
    public class SexViewComponent : ViewComponent
    {

        private IBurialRepository repo { get; set; }
        public SexViewComponent(IBurialRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedSex = RouteData?.Values["sex"];
            var sex = repo.burialmain
                .Select(x => x.sex)
                .Distinct()
                .OrderBy(x => x);

            return View(sex);
        }
    }
}
