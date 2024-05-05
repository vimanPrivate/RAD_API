using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace UserExperienceAnalizer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    string key = "q7N1bx2KdqKgrF0lkULSnuqsFQlHEc0yjyVKgaOW";
        //    string url = "https://userexperianceanalyzer-default-rtdb.firebaseio.com/";
        //    string appName = "UserExAnalyser";

        //    IFirebaseClient client;

        //    IFirebaseConfig config = new FirebaseConfig
        //    {
        //        AuthSecret = key,
        //        BasePath = url,
        //    };

        //    client = new FireSharp.FirebaseClient(config);

        //    var data = new
        //    {
        //        Name = "Viman",
        //        Age = 29,
        //    };


        //    //var response = client.Push("Doc/", data);

        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public IEnumerable<WeatherForecast> Test()
        {
            HttpContext.Items["userId"] = "val 123";

            string key = "q7N1bx2KdqKgrF0lkULSnuqsFQlHEc0yjyVKgaOW";
            string url = "https://userexperianceanalyzer-default-rtdb.firebaseio.com/";
            string appName = "UserExAnalyser";

            IFirebaseClient client;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = key,
                BasePath = url,
            };

            client = new FireSharp.FirebaseClient(config);

            var data = new
            {
                Name = "Viman",
                Age = 29,
            };


            //var response = client.Push("Doc/", data);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
