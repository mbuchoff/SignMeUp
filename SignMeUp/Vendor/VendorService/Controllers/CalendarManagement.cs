using Microsoft.AspNetCore.Mvc;
using SafemarkGoAdminTool;
using System.Threading.Tasks;
using Common.Models;
using Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

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

        [HttpGet]
        public async Task<IEnumerable<Schedule>> GetSchedules(DateTime start, DateTime end) => 
            await db.Schedules.Where(x => x.End >= start && x.Start <= end).ToListAsync();
    }
}
