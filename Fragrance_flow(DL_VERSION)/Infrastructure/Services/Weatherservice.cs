using Fragrance_flow_DL_VERSION_.Application.interfaces;
using Fragrance_flow_DL_VERSION_.Domain.Entities;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace Fragrance_flow_DL_VERSION_.Infrastructure.Services
{
    public class Weatherservice : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IWeatherService _weather;
        public Weatherservice(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        // This gets users generel location from ip-api.com and puts the data to ipLocation class then checks the-
        // current weather based on the given location.
        public async Task<double?> UserLocationAsync()
        {
            try
            {
                var location = await _httpClient.GetFromJsonAsync<IpLocation>("http://ip-api.com/json/");

                if (location != null && location.status == "success")
                {

                    var weatherUrl = $" https://api.open-meteo.com/v1/forecast?latitude={location.lat.ToString(CultureInfo.InvariantCulture)}&longitude={location.lon.ToString(CultureInfo.InvariantCulture)}&current_weather=true&temperature_unit=celsius";

                    var response = await _httpClient.GetAsync(weatherUrl);
                    if (!response.IsSuccessStatusCode) return 0;

                    var json = await response.Content.ReadAsStringAsync();

                    using var document = JsonDocument.Parse(json);
                    var currentWeather = document.RootElement
                        .GetProperty("current_weather");

                    var temperature = currentWeather
                        .GetProperty("temperature").GetDouble();
                    return temperature;
                }
                else
                {
                    Console.WriteLine(" Unable to fetch IP location.");
                    Console.WriteLine($" Status: {location.status}");
                }
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(" An error occured during the http request : " + hrex.Message);
            }
            catch (JsonException jex)
            {
                Console.WriteLine(" An error occured while getting the json response : " + jex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" An error occurred while fetching IP location: " + ex.Message);
            }
            return null;
        }
    }

}
