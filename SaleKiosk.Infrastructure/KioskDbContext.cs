using Microsoft.EntityFrameworkCore;
using SaleKiosk.Domain.Models;

namespace SaleKiosk.Infrastructure
{
    public class KioskDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public KioskDbContext(DbContextOptions<KioskDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
