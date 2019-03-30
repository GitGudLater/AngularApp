using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngApp.EntityModels;
using Microsoft.EntityFrameworkCore;


namespace AngApp.Services
{
    public class DataService
    {
        FullContext db;

        public DataService(FullContext fullContext)
        {
            db = fullContext;
            if (!db.Products.Any())
            {
                db.Products.Add(new Product { Name = "iPhone X", Designer = "Apple", Cost = 79900, About = "Premium class smartphone" });
                db.Products.Add(new Product { Name = "Galaxy S8", Designer = "Samsung", Cost = 49900, About = "Flagman Samsung smartphone with better ergonomics abilities" });
                db.Products.Add(new Product { Name = "Pixel 2", Designer = "Google", Cost = 52900, About = "Most powered smartphone" });
                db.SaveChanges();
            }
        }

        public IEnumerable<FullList> Get(string _username, bool personal) {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            List<Product> products = db.Products.ToList();
            List<FullList> fullList = new List<FullList>();
            int count = products.Count;
            for (int i = 0; i < count; i++)
            {
                fullList[i] = new FullList() { Id = products[i].Id, Name = products[i].Name, Designer = products[i].Designer, About = products[i].About, Cost = products[i].Cost };
            }
            return fullList;
        }

        public IEnumerable<FullList> Get(string username) {
            List<Product> products = db.Products.ToList();
            List<FullList> fullList = new List<FullList>();
            int count = products.Count;
            for (int i = 0; i < count; i++)
            {
                fullList[i] = new FullList() { Id = products[i].Id, Name = products[i].Name, Designer = products[i].Designer, About = products[i].About, Cost = products[i].Cost };
                User compareduser = products[i].Users.First(x => x.Email == username);
                if (compareduser != null)
                    fullList[i].Checked = true;
                else
                    fullList[i].Checked = false;
            }
            return fullList;
        }

        public IEnumerable<FullList> Get() {
            List<Product> products = db.Products.ToList();
            List<FullList> fullList = new List<FullList>();
            int count = products.Count;
            for(int i=0;i<count;i++)
            {
                fullList[i] = new FullList() { Id = products[i].Id, Name = products[i].Name, Designer = products[i].Designer, About = products[i].About, Cost = products[i].Cost };
            }
            return fullList;
        }

        public void Toggle(int id,string username) {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            User currentuser = product.Users.FirstOrDefault(x => x.Email == username);
            if (currentuser != null)
            {
                product.Users.Remove(currentuser);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                product.Users.Add(currentuser);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Set(FullList viewproduct) {
            Product product = new Product() { Name = viewproduct.Name, Designer = viewproduct.Designer, Cost = viewproduct.Cost, About = viewproduct.About };
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Change(FullList viewproduct) {
            Product product = db.Products.First(x => x.Id == viewproduct.Id);
            product.Name = viewproduct.Name;
            product.Designer = viewproduct.Designer;
            product.Cost = viewproduct.Cost;
            product.About = viewproduct.About;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id) {
            Product product = db.Products.First(x => x.Id == id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
