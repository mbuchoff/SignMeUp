using ClientSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VendorService.Models;

namespace ClientSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this._logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<bool> AddSchedule([FromBody]Schedule schedule)
        {
            var client = httpClientFactory.CreateClient("VendorService");
            var body = JsonConvert.SerializeObject(schedule);

            HttpResponseMessage response = await client.PostAsync("CalendarManagement/CreateSchedule", new StringContent(body, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
