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

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Parcel/{trackingUri}")]
        public IActionResult TrackParcel(string trackingUri)
        {
            if (!string.IsNullOrEmpty(trackingUri))
            {
                return RedirectToAction("Index", "/", new { trackingUri });
            }

            return View("/Index");
        }

        [HttpGet("/Home/UpdateSubscribtion")]
        public async Task<IActionResult> UpdateSubscribtion(string topic)
        {
            await _mqttService.UpdateSubscribtion(topic);
            return View("Index");
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

        [Route("/Postman")]
        public IActionResult Postman()
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
