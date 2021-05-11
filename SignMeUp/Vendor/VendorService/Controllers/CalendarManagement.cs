using Microsoft.AspNetCore.Mvc;
using SafemarkGoAdminTool;
using System.Threading.Tasks;
using Common.Models;

namespace VendorService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CalendarManagementController : ControllerBase
    {
        public CalendarManagementController()
        {

        }

        [HttpPost]
        public void CreateSchedule(/*[FromBody]Schedule schedule*/)
        {

        }
    }
}
