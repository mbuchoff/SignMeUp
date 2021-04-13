using Microsoft.AspNetCore.Mvc;
using VendorService.Models;

namespace VendorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarManagementController : ControllerBase
    {
        [HttpPost]
        public void CreateSchedule(Schedule schedule)
        {

        }
    }
}
