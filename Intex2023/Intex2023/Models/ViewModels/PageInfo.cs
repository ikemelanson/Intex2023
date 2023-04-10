using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2023.Models.ViewModels
{
    public class PageInfo
    {

        public int TotalNumRecords { get; set; }
        public int BurialsPerPage { get; set; }
        public int CurrentPage { get; set; }


        //Figure out how many pages we need
        public int TotalPages => (int)Math.Ceiling((double)TotalNumRecords / BurialsPerPage);
    }
}
