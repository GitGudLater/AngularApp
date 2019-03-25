using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public bool Checked { get; set; }
    }
}
