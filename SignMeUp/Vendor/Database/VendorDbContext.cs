using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace Database
{
    public class VendorDbContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
    }
}
