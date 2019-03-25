using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AngApp.Models
{
    public class FullContext:DbContext
    {
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public FullContext(DbContextOptions<FullContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
