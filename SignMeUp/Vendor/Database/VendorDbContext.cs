using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvailabilityLookup>().HasData(
                new AvailabilityLookup { Id = 1, Name = "Free" },
                new AvailabilityLookup { Id = 2, Name = "Busy" });
        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<AvailabilityLookup> AvailabilityLookups { get; set; }
    }
}
