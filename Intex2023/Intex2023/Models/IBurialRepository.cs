using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2023.Models
{
    public interface IBurialRepository
    {
        IQueryable<Burial> burialmain { get; }
    }
}
