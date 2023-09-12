using CleanZone.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanZone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Residence> Residence { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<CleanLog> CleanLogs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

//User.FindFirstValue