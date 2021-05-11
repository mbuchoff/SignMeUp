using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace Database
{
    public class VendorDbContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql-signmeup-vendor-dev-centralus.database.windows.net;Database=db-signmeup-vendor-dev-centralus;User ID=vendor_app_service;Password=0MK%mjwcA_VVvMTnV#iKJFnrC0IT0HEW;Trusted_Connection=False;Encrypt=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
