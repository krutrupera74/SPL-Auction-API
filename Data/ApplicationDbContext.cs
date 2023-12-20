using auction.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace auction.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Players> Players { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
