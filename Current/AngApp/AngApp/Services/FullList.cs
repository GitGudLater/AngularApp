using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designer { get; set; }
        public string About { get; set; }
        public int Cost { get; set; }
        public bool Favourite { get; set; }
    }

    public class AddPhoneDto
    {
        public string Name { get; set; }
        public string Designer { get; set; }
        public string About { get; set; }
        public int Cost { get; set; }
    }
}
