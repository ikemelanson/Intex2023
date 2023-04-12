using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2023.Models
{
    public class EFBurialRepository : IBurialRepository
    {
        private BurialContext context { get; set; }
        public EFBurialRepository(BurialContext temp)
        {
            context = temp;
        }
        public IQueryable<Burial> burialmain => context.burialmain;
    }
}
