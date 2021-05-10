using Microsoft.AspNetCore.Mvc;
using SafemarkGoAdminTool;
using System.Threading.Tasks;
using VendorService.Models;

namespace VendorService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CalendarManagementController : ControllerBase
    {
        private readonly AzureKeyVaultService azureKeyVaultService;

        public CalendarManagementController(AzureKeyVaultService azureKeyVaultService)
        {
            this.azureKeyVaultService = azureKeyVaultService;
        }

        [HttpPost]
        public async Task CreateSchedule([FromBody]Schedule schedule)
        {
            var doesThisWork = await azureKeyVaultService.DbConnectionString;
        }
    }
}
