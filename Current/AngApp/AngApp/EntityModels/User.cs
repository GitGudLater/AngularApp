using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<ProductUser> ProductUsers { get; set; }

        public User()
        {
            ProductUsers = new List<ProductUser>();
        }
    }
}
