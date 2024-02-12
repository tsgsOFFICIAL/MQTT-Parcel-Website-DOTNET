using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MQTT_Parcel_Website.Models;
using MQTT_Parcel_Website.Services;

namespace MQTT_Parcel_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MqttService _mqttService;

        public async Task<IActionResult> Index()
        {
            await _mqttService.ConnectClient();
            ViewBag.Payload = _mqttService.GetPayload();
            return View();
        }

        public IActionResult GetPayload()
        {
            return Content(_mqttService.GetPayload());
        }

        public HomeController(ILogger<HomeController> logger, MqttService mqttService)
        {
            _logger = logger;
            _mqttService = mqttService;
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
    }
}
