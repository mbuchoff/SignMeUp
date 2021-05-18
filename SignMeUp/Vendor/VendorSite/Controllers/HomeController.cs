using ClientSite.Models;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace ClientSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
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

        [HttpGet]
        public async Task<IEnumerable<Schedule>> GetSchedules(DateTime start, DateTime end)
        {
            var client = httpClientFactory.CreateClient("VendorService");
            HttpResponseMessage response = await client.GetAsync($"CalendarManagement/GetSchedules?start={start.ToJsonString()}&end={end.ToJsonString()}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Schedule>>(responseBody);
        }
    }
}
