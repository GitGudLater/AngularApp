using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace AngApp.EntityModels
{
    public class FullContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public FullContext(DbContextOptions<FullContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
