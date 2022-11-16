using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using sakilaAppMySQL.Dtos.ActorsDto;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using System.Net.Http.Headers;
using sakilaAppMySQL.Dtos.Authentication;
using System;
using System.Security.Policy;
using System.Text;
using System.Security.Cryptography;

namespace sakilaAppMySQL.Controllers
{
    [ApiController]
    [Route("serverb/api")]
    public class ServerBController : ControllerBase
    {
        private const string HOST = "https://localhost:7280";
        private const string SECRET_KEY = "ServerBAuthenticationSecretKey";

        private readonly IConfiguration configuration;

        public ServerBController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet()]
        [Route("weatherforecast")]
        public async Task<IActionResult> Get()
        {
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            using (var client = this.RequestHandler(HOST, "/WeatherForecast"))
            {
                HttpResponseMessage Res = await client.GetAsync("WeatherForecast");

                if (Res.IsSuccessStatusCode)
                {
                    var responseObject = Res.Content.ReadAsStringAsync().Result;
                    weatherForecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(responseObject);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Internal server error" });
                }
                return Ok(weatherForecasts);
            }
        }

        public HttpClient RequestHandler(string host, string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(host + uri);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

            String authenticationDataString = (String.Format("{0}{1}", uri, requestTimeStamp));

            string secretKey = configuration.GetValue<String>(SECRET_KEY);
            string hashedToken = ComputeHash(secretKey,authenticationDataString);

            client.DefaultRequestHeaders.Add("TimeStamp", requestTimeStamp);
            client.DefaultRequestHeaders.Add("XApiKey", hashedToken);

            return client;
        }

        private static string ComputeHash(String secretKey, String authenticationDataString)
        {
            HMACSHA512 hmac = new HMACSHA512(Convert.FromBase64String(secretKey));

            Byte[] authenticationData = UTF8Encoding.GetEncoding("utf-8").GetBytes(authenticationDataString);

            var hashedToken = hmac.ComputeHash(authenticationData);
            return Convert.ToBase64String(hashedToken);
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
