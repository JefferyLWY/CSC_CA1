using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4_5.Models;

namespace Task4_5.Data
{
    public class TalentDbContext : DbContext
    {
        public TalentDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Talent> Talents { get; set; }
    }
}
