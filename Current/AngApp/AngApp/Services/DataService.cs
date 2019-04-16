using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngApp.EntityModels;
using Microsoft.EntityFrameworkCore;


namespace AngApp.Services
{
    public class PhonesCatalog : IPhonesCatalog
    {
        FullContext db;

        public PhonesCatalog(FullContext fullContext)
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
        //Postman
        public IEnumerable<PhoneDto> GetFullCatalog(string userName)
        {
            List<Product> products = db.Products.Include(x => x.ProductUsers).ToList();
            List<PhoneDto> fullList = new List<PhoneDto>();
            if (userName==null)
            {
                int count = products.Count;
                for (int i = 0; i < count; i++)
                {
                    fullList.Add(new PhoneDto() { Id = products[i].Id, Name = products[i].Name, Designer = products[i].Designer, About = products[i].About, Cost = products[i].Cost });
                }
                return fullList;
            }
            else
            {
                User user = db.Users.First(x => x.Email == userName);
                if (user == null)
                    throw new ServiceException("User not exist");
                int count = products.Count;
                for (int i = 0; i < count; i++)
                {
                    fullList.Add(new PhoneDto() { Id = products[i].Id, Name = products[i].Name, Designer = products[i].Designer, About = products[i].About, Cost = products[i].Cost });
                    ProductUser relation = products[i].ProductUsers.FirstOrDefault(x => x.UserId == user.Id);
                    if (relation != null)
                        fullList[i].Favourite = true;
                    else
                        fullList[i].Favourite = false;
                }
                return fullList;
            }
        }

        public IEnumerable<PhoneDto> GetFavoriteList(string userName)
        {
            if (userName==null)
                throw new ServiceException("Incorrect username value(must be not null)");
            List<Product> products = db.Products.Include(x => x.ProductUsers).ToList();
            List<PhoneDto> fullList = new List<PhoneDto>();
            User user = db.Users.First(x => x.Email == userName);
            if (user == null)
                throw new ServiceException("Current user not exist");
            int count = products.Count;
            for (int i = 0; i < count; i++)
            {
                ProductUser relation = products[i].ProductUsers.FirstOrDefault(x => x.UserId == user.Id);
                if (relation != null)
                    fullList.Add(new PhoneDto() { Id = products[i].Id, Name = products[i].Name, Designer = products[i].Designer, About = products[i].About, Cost = products[i].Cost , Favourite = true});
            }
            return fullList;
        }

        public void ToggleFavoriteFlag(int id, string username)
        {
            if(username==null)
                throw new ServiceException("Incorrect username value(must be not null)");
            Product product = db.Products.Include(x => x.ProductUsers).First(x => x.Id == id);
            if (product == null)
                throw new ServiceException("Cuurent product not exist");
            User currentuser = db.Users.First(x => x.Email == username);
            if (currentuser == null)
                throw new ServiceException("Cuurent user not exist");
            ProductUser relation = product.ProductUsers.FirstOrDefault(x => x.UserId == currentuser.Id);
            if (relation != null)
            {
                product.ProductUsers.Remove(relation);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                relation = new ProductUser() { ProductId = product.Id , UserId = currentuser.Id};
                product.ProductUsers.Add(relation);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Add(AddPhoneDto viewproduct)
        {
            Product product = new Product() { Name = viewproduct.Name, Designer = viewproduct.Designer, Cost = viewproduct.Cost, About = viewproduct.About };
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Change(PhoneDto viewproduct)
        {
            Product product = db.Products.First(x => x.Id == viewproduct.Id);
            if (product == null)
                throw new ServiceException("Current product not exist");
            product.Name = viewproduct.Name;
            product.Designer = viewproduct.Designer;
            product.Cost = viewproduct.Cost;
            product.About = viewproduct.About;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = db.Products.First(x => x.Id == id);
            if (product == null)
                throw new ServiceException("Current product not exist");
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }

    class ServiceException:Exception
    {
        public ServiceException(string message): base(message)
        { }
    }
}
