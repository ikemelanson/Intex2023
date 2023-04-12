using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Intex2023.Models
{
    public class BurialContext : DbContext
    {
        public BurialContext()
        {
        }

        public BurialContext(DbContextOptions<BurialContext> options)
            : base(options)
        {
        }

        public DbSet<Burial> burialmain { get; set; }

    }
}
