using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using AngApp.Models;
using AngApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AngApp.Controllers
{
    [Route("api/[controller]")]
    public class RelationController : Controller
    {

        FullContext db;

        public RelationController(FullContext context)
        {
            db = context;
        }

        //[Authorize]
        [HttpGet]
        public IEnumerable<Relation> Get()
        {
            try
            {
                List<Relation> list = db.Relations.Where(x => x.UserName == User.Identity.Name).ToList();
                return list;
            }
            catch
            {
                List<Relation> list = new List<Relation>();
                return list;
            }
        }


        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]ViewRelation relation)
        {
            Relation buffer = db.Relations.FirstOrDefault((Relation x) => x.UserName == User.Identity.Name && x.ProductId == relation.ProductId);
            if(buffer!=null)
            {
                string username = User.Identity.Name;
                Relation product = db.Relations.FirstOrDefault(x => x.ProductId == relation.ProductId && x.UserName == username && x.Checked == true);
                db.Relations.Remove(product);
                db.SaveChanges();
            }
            Relation dbrelationmodel = new Relation() { ProductId = relation.ProductId, Checked = true, UserName = User.Identity.Name };
            db.Relations.Add(dbrelationmodel);
            db.SaveChanges();
            return Ok(relation);
        }

        /*[Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ViewRelation relation)
        {
            if (ModelState.IsValid)
            {
                Relation dbrelation = new Relation
                db.Update(relation);
                db.SaveChanges();
                return Ok(relation);
            }
            return BadRequest(ModelState);
        }*/

        /*
        public IActionResult Index()
        {
            return View();
        }
        */
    }

}