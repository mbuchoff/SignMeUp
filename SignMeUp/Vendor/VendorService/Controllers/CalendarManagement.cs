using Microsoft.AspNetCore.Mvc;
using VendorService.Models;

namespace VendorService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CalendarManagementController : ControllerBase
    {
        [HttpPost]
        public void CreateSchedule([FromBody]Schedule schedule)
        {

        }
    }
}
