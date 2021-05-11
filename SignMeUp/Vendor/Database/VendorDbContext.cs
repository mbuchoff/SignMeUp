using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace Database
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options) : base(options)
        {

        }

        public DbSet<Schedule> Schedules { get; set; }
    }
}
