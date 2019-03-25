using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace AngApp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        FullContext db;

        public SampleDataController(FullContext context)
        {
            db = context;
            if (!db.Products.Any())
            {
                db.Products.Add(new Product { Name = "iPhone X", Designer = "Apple", Cost = 79900, About = "Premium class smartphone" });
                db.Products.Add(new Product { Name = "Galaxy S8", Designer = "Samsung", Cost = 49900, About = "Flagman Samsung smartphone with better ergonomics abilities" });
                db.Products.Add(new Product { Name = "Pixel 2", Designer = "Google", Cost = 52900, About = "Most powered smartphone" });
                db.SaveChanges();
            }
        }

        /*private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };*/

        //[Authorize]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        /*HttpGet("[action]")]
        public IEnumerable<Product> WeatherForecasts()
        {
            return db.Products.ToList();
        }*/

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }


        //!!!!!
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            List<Relation> relations = db.Relations.Where(x => x.ProductId == id).ToList();
            int count = relations.Count;
            for(int j=0;j<relations.Count;j++)
            {
                db.Relations.Remove(relations[j]);
                db.SaveChanges();
            }
            return Ok(product);
        }

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
