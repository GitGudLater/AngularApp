using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PracticalApp.Models;
using PracticalApp.Services;


namespace PracticalApp.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {

        IDataService service;

        public DataController(IDataService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IEnumerable<FullList> GetPhones([FromBody]bool personal)
        {
            return service.GetPhones(User.Identity.Name, personal);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            service.Delete(id);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct([FromBody]FullList product)
        {
            service.Set(product);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult MarkAsFavourite(int id)
        {
            service.Toggle(id, User.Identity.Name);
            return Ok();
        }

        [Authorize]
        [HttpPatch]
        public IActionResult UpdateProduct([FromBody]FullList fullList)
        {
            service.Change(fullList);
            return Ok();
        }
       /* private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };*/

        /*[HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }*/

        /*public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }*/
    }
}
