using Microsoft.EntityFrameworkCore;
using sem7_SE_project.Models;

namespace sem7_SE_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { }

        public DbSet<Brand>? Brands { get; set; }

        public DbSet<Model>? Models { get; set; }

        public DbSet<EmbeddedDevice>? EmbeddedDevices { get; set; }

        public DbSet<EngineType>? EngineTypes { get; set; }

        public DbSet<Client>? Clients { get; set; }

        public DbSet<Car>? Cars { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<User>? Users { get; set; }

        public DbSet<OrderStatus>? OrderStatuses { get; set; }
    }
}
