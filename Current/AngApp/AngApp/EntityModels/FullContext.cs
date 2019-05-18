using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AngApp.EntityModels
{
    public class FullContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public IConfiguration Configuration { get; }

        public FullContext(DbContextOptions<FullContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductUser>()
                .HasKey(t => new { t.ProductId, t.UserId });

            modelBuilder.Entity<ProductUser>()
                .HasOne(sc => sc.Product)
                .WithMany(s => s.ProductUsers)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<ProductUser>()
                .HasOne(sc => sc.User)
                .WithMany(c => c.ProductUsers)
                .HasForeignKey(sc => sc.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationExtensions.GetConnectionString(Configuration,"DefaultConnection"));
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=thirdfulldb;Trusted_Connection=True;");
        }

    }
}
