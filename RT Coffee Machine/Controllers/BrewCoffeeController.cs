using Microsoft.AspNetCore.Mvc;
using RT_Coffee_Machine.Client;
using RT_Coffee_Machine.Models;
using RT_Coffee_Machine.Services;
using RT_Coffee_Machine.Utils;

namespace RT_Coffee_Machine.Controllers
{
    public class BrewCoffeeController : Controller
    {
        private double LIMIT_TEMPERATURE = 30.0;
        private readonly ILogger<BrewCoffeeController> _logger;
        private readonly ICountCoffeeService _countService;
        private readonly OpenWeatherHttpClient _owClient;
        public BrewCoffeeController(
            ILogger<BrewCoffeeController> logger,
            ICountCoffeeService countService,
            OpenWeatherHttpClient client)
        {
            _logger = logger;
            _countService = countService;
            _owClient = client;
        }

        [HttpGet]
        [Route("brew-coffee")]
        public async Task<IActionResult> GetBrewCoffeAsync()
        {
            // check if is month 4 (April) and day 1
            if (DateTime.Today.IsMonthDay(4, 1))
            {
                return new ObjectResult("")
                {
                    StatusCode = StatusCodes.Status418ImATeapot
                };
            }

            // increase coffe count
            var currentCoffeCount = _countService.IncreaseCoffeCount();
            if (currentCoffeCount == 5)
            {
                _logger.LogWarning("fifth coffe called => Reset count and return 503 Service Unavailable");
                _countService.ResetCount();

                return new ObjectResult("")
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable
                };
            }

            _logger.LogInformation("Coffe is comming for pos {0}", currentCoffeCount);

            GetBrewCoffeResponse response = new GetBrewCoffeResponse()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTimeOffset.Now
            };
            if (await _owClient.GetTemperatureOfCity() > LIMIT_TEMPERATURE)
            {
                response.Message = "Your refreshing iced coffee is ready";
            }
            return Ok(response);
        }
    }
}
