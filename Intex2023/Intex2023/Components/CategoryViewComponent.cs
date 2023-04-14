using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2023.Models;
//Manages the burial hair color filter
namespace Intex2023.Components
{
    public class CategoryViewComponent : ViewComponent
    {

        private IBurialRepository repo { get; set; }
        public CategoryViewComponent (IBurialRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["burialhaircolor"];
            var cats = repo.burialmain
                .Select(x => x.haircolor)
                .Distinct()
                .OrderBy(x => x);

            return View(cats);
        }
    }
}
