using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2023.Models.ViewModels
{
    public class BurialsViewModel
    {
        public IQueryable<Burial> Burials { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
