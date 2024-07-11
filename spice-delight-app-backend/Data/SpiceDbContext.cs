using Microsoft.EntityFrameworkCore;
using spice_delight_app_backend.Models;

namespace spice_delight_app_backend.Data
{
    public class SpiceDbContext : DbContext
    {
        public SpiceDbContext(DbContextOptions<SpiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Stripe> Stripe { get; set; }


    }
}