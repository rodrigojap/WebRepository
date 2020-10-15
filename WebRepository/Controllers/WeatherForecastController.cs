using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRepository.Infra.Contracts;
using WebRepository.Notifications.Contracts;

namespace WebRepository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWebRepository WebRepository;
        private readonly IAPINotification Notification;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                         IWebRepository webRepository,
                                         IAPINotification notification)
        {
            _logger = logger;

            WebRepository = webRepository;
            Notification = notification;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var myWheaterParams = new Dictionary<string, object>();
            myWheaterParams.Add("id", id);

            var myWheatherForecast = await WebRepository.GetAsync<WeatherForecast>(myWheaterParams, "GetWheaterForeCast");

            if (Notification.HasNotification())
            {
                return BadRequest(
                    new
                    {
                        success = false,
                        message = Notification.GetNotifications().ToArray()
                    });
            }

            return Ok(
                new
                {
                    success = true,
                    message = "Request complete!",
                    model = myWheatherForecast
                });
        }
    }
}
