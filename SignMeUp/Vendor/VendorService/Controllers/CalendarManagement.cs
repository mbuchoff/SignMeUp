using Microsoft.AspNetCore.Mvc;
using SafemarkGoAdminTool;
using System.Threading.Tasks;
using Common.Models;
using Database;

namespace VendorService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CalendarManagementController : ControllerBase
    {
        private readonly VendorDbContext db;

        public CalendarManagementController(VendorDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task CreateSchedule([FromBody]Schedule schedule)
        {
            await db.Schedules.AddAsync(schedule);
            await db.SaveChangesAsync();
        }
    }
}
