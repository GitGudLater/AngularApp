using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designer { get; set; }
        public int Cost { get; set; }
        public string About { get; set; }
    }
}
