using auction.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace auction.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<Tournaments> Tournaments { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Sports> Sports { get; set; }
    }
}
