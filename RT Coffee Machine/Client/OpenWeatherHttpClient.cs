using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Web;

namespace RT_Coffee_Machine.Client
{
    public class OpenWeatherHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public OpenWeatherHttpClient(
            HttpClient client,
            IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        private Uri CreateWeatherUri()
        {
            var config = _configuration.GetSection("OpenWeather");

            var uriBuilder = new UriBuilder("https://api.openweathermap.org/data/2.5/weather");

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["q"] = config.GetValue<string>("Q");
            query["units"] = config.GetValue<string>("Units");
            query["appid"] = config.GetValue<string>("AppId");
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }

        public async Task<double> GetTemperatureOfCity()
        {
            var uri = CreateWeatherUri();

            var response = await _client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var c = JsonNode.Parse(content);
            dynamic stuff = JObject.Parse(content);
            try
            {
                return (double)stuff?.main?.temp;
            }
            catch
            {
                throw new Exception("Cannot get temperature of city!");
            }

        }
    }
}
