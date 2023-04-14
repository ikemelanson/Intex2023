using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex2023.Models;
//Manages the north south filter
namespace Intex2023.Components
{
    public class NorthSouthViewComponent : ViewComponent
    {

        private IBurialRepository repo { get; set; }
        public NorthSouthViewComponent(IBurialRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedNorthSouth = RouteData?.Values["northsouth"];
            var northsouth = repo.burialmain
                .Select(x => x.northsouth)
                .Distinct()
                .OrderBy(x => x);

            return View(northsouth);
        }
    }
}
