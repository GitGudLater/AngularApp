using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designer { get; set; }
        public int Cost { get; set; }
        public string About { get; set; }
        public ICollection<User> Users { get; set; }
        public Product()
        {
            Users = new List<User>();
        }
    }
}
