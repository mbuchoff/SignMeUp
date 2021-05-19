using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ExternalModels = Common.Models;
using DatabaseModels = Database.Models;
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
        public async Task CreateSchedule([FromBody]ExternalModels.Schedule schedule)
        {
            var availabilityStr = schedule.Availability == ExternalModels.Availability.Free ? "Free" : "Busy";
            var availabilityLookup = await db.AvailabilityLookups.FirstAsync(x => x.Name == availabilityStr);
            await db.Schedules.AddAsync(new DatabaseModels.Schedule
            {
                ExternalId = schedule.Id,
                Title = schedule.Title,
                Location = schedule.Location,
                AvailabilityLookup = availabilityLookup,
                Start = schedule.Start,
                End = schedule.End,
            });

            await db.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<ExternalModels.Schedule>> GetSchedules(DateTime start, DateTime end)
        {
            var dbSchedules = await db.Schedules.Where(x => x.End >= start && x.Start <= end).ToListAsync();
            return dbSchedules.Select(schedule =>
            {
                var availability = schedule.AvailabilityLookup.Name == DatabaseModels.AvailabilityLookup.BUSY_NAME ? ExternalModels.Availability.Busy : ExternalModels.Availability.Free;

                return new ExternalModels.Schedule
                {
                    Id = schedule.ExternalId,
                    Title = schedule.Title,
                    Location = schedule.Location,
                    Availability = availability,
                    Start = schedule.Start,
                    End = schedule.End,
                };
            });
        }
    }
}
