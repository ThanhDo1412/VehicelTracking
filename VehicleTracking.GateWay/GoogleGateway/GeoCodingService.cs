using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.GateWay.GoogleGateway;

namespace VehicleTracking.Gateway.GoogleGateway
{
    public class GeoCodingService : IGeoCodingService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public GeoCodingService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiUrl = _configuration["GoogleToken:GoogleUrl"];
        }

        //TODO: will be change it to request and response model later
        public async Task<string> ReverseGeoCoding(decimal lon, decimal lat)
        {
            var key = _configuration["GoogleToken:API_KEY"];
            var url = $"geocode/json?latlng={lat},{lon}&location_type=ROOFTOP&key={key}";

            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await SendAsync(HttpMethod.Get, url, content);

            var json = await response.Content.ReadAsStringAsync();
            var objectConverted = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

            if (objectConverted.ContainsKey("results") && objectConverted["results"][0] != null)
            {
                return objectConverted["results"][0].formatted_address;
            }

            return "";
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string requestUrl, HttpContent content)
        {
            HttpClient client = BuildHttpClient(_apiUrl);

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(client.BaseAddress, requestUrl),
                Content = content
            };

            var response = await client.SendAsync(request);

            return response;
        }

        private HttpClient BuildHttpClient(string apiUrl)
        {
            var httpClientInstance = new HttpClient()
            {
                BaseAddress = new Uri(apiUrl),
                Timeout = TimeSpan.FromMinutes(5)
            };

            httpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClientInstance;
        }
    }
}
