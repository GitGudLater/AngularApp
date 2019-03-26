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
    public class ChosenController : Controller
    {
        FullContext db;

        public ChosenController(FullContext context)
        {
            db = context;
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            if (User.Identity.Name == null)
            {
                List<Product> list = new List<Product>();
                return list;
            }
            List<Relation> relations = db.Relations.Where(x => x.UserName == User.Identity.Name).ToList();
            List<Product> result = new List<Product>();
            for (int i = 0; i < relations.Count; i++)
            {
                result.Add(db.Products.FirstOrDefault(x => x.Id == relations[i].ProductId));
            }
            return result;
        }
    }
}