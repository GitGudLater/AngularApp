using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AngApp.EntityModels;
using AngApp.Services;

namespace AngApp.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {

        IPhonesCatalog service;

        public DataController(IPhonesCatalog _service)
        {
            service = _service;
        }

        [HttpGet]
        public string AuthorizeUser()
        {
            string name = User.Identity.Name;
            return name;
        }


        [HttpGet("false")]
        public IEnumerable<PhoneDto> GetPhonesCatalog()
        {
            return service.GetFullCatalog(User.Identity.Name);
        }

        [HttpGet("true")]
        public IEnumerable<PhoneDto> GetFavoritePhones()
        {
            return service.GetFavoriteList(User.Identity.Name);
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
        public IActionResult AddProduct([FromBody]AddPhoneDto product)
        {
            service.Add(product);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult MarkAsFavourite(int id)
        {
            service.ToggleFavoriteFlag(id, User.Identity.Name);
            return Ok();
        }

        [Authorize]
        [HttpPatch]
        public IActionResult UpdateProduct([FromBody]ChangePhoneDto fullList)
        {
            service.Change(fullList);
            return Ok();
        }
    }

}