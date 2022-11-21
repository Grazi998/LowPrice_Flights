using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LowPrice_Flights_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public FlightsController(IConfiguration config)
        {
            _config = config;
        }

        public class TokenInfo
        {
            public string? type { get; set; }
            public string? username { get; set; }
            public string? application_name { get; set; }
            public string? client_id { get; set; }
            public string? token_type { get; set; }
            public string? access_token { get; set; }
            public string? expires_in { get; set; }
            public string? state { get; set; }
            public string error_message { get; set; } = "Token does not exist!";

        }

        public async Task<JsonResult> GetAccessToken()
        {
            var APIUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";
            var APIKey = _config.GetValue<string>("AmadeusSSAPI:APIKey");
            var APISecret = _config.GetValue<string>("AmadeusSSAPI:APISecret");

            var bodyData = new StringContent(String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}", APIKey, APISecret), Encoding.UTF8, "application/x-www-form-urlencoded");

            using var client = new HttpClient();

            var response = await client.PostAsync(APIUrl, bodyData);

            string result = response.Content.ReadAsStringAsync().Result;

            return new JsonResult(result);
        }

        [HttpPost("GetFlights")]
        public async Task<JsonResult> GetFlights(SearchParameters parameters)
        {
            var token = await GetAccessToken();

            TokenInfo tokenInfo = new TokenInfo();

            if (token.Value != null)
            {
                tokenInfo = JSONParser.FromJson<TokenInfo>(token.Value.ToString());
                tokenInfo.error_message = "";
            }

            if (!string.IsNullOrEmpty(tokenInfo.error_message))
            {
                return new JsonResult(tokenInfo.error_message);
            }

            string searchURL = String.Format("https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode={0}&destinationLocationCode={1}&departureDate={2}&returnDate={3}&adults={4}&currencyCode={5}",
                parameters.OriginAirport,
                parameters.DestinationAirport,
                parameters.DepartureDate,
                parameters.ReturnDate,
                parameters.PassangersNumber,
                parameters.CurrencyCode);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, searchURL);

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.access_token);

            var httpClient = new HttpClient();

            var response = await httpClient.SendAsync(requestMessage);

            var result = response.Content.ReadAsStringAsync().Result;

            return new JsonResult(JSONParser.FromJson<Object>(result.ToString()));
        }
    }
}
