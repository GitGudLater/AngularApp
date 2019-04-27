using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngApp.EntityModels;
using Microsoft.EntityFrameworkCore;
using AngApp.Services.Exceptions;
using AngApp.ViewModels.ProductModels;



namespace AngApp.Services
{
    public class PhonesCatalogService : IPhonesCatalogService
    {
        FullContext db;

        public PhonesCatalogService(FullContext fullContext)
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

        public IEnumerable<PhoneDto> GetFullCatalog(string userName)
        {
            List<Product> products = db.Products.Include(x => x.ProductUsers).ToList();
            List<PhoneDto> fullList = new List<PhoneDto>();

            if (userName==null)
            {
                int count = products.Count;
                foreach(var product in products)
                {
                    fullList.Add(new PhoneDto() { Id = product.Id, Name = product.Name, Designer = product.Designer, About = product.About, Cost = product.Cost });
                }

                return fullList;
            }
            else
            {
                User user = db.Users.FirstOrDefault(x => x.Email == userName);
                if (user == null)
                    throw new UserNotExistException("User not exist");

                foreach(var product in products)
                {
                    ProductUser relation = product.ProductUsers.FirstOrDefault(x => x.UserId == user.Id);
                    bool checkForFavorite;
                    if (relation != null)
                        checkForFavorite = true;
                    else
                        checkForFavorite = false;

                    fullList.Add(new PhoneDto() { Id = product.Id, Name = product.Name, Designer = product.Designer, About = product.About, Cost = product.Cost, Favourite=checkForFavorite });

                }

                return fullList;
            }
        }

        public IEnumerable<PhoneDto> GetFavoriteList(string userName)
        {
            if (userName==null)
            {
                throw new UserNameNullException("Username must be not null");
            }

            List<Product> products = db.Products.Include(x => x.ProductUsers).ToList();
            List<PhoneDto> fullList = new List<PhoneDto>();
            User user = db.Users.First(x => x.Email == userName);

            if (user == null)
                throw new UserNotExistException("Current user not exist");

            foreach(var product in products)
            {
                ProductUser relation = product.ProductUsers.FirstOrDefault(x => x.UserId == user.Id);
                if (relation != null)
                    fullList.Add(new PhoneDto() { Id = product.Id, Name = product.Name, Designer = product.Designer, About = product.About, Cost = product.Cost, Favourite = true });
            }

            return fullList;
        }

        public void ToggleFavoriteFlag(int id, string username)
        {
            if (username == null)
            {
                throw new UserNameNullException("Incorrect username value(must be not null)");
            }

            Product product = db.Products.Include(x => x.ProductUsers).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new ProductNotExistException("Cuurent product not exist");
            }

            User currentuser = db.Users.FirstOrDefault(x => x.Email == username);
            if (currentuser == null)
            {
                throw new UserNotExistException("Cuurent user not exist");
            }

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

        public void Add(AddPhoneDto addInput)
        {
            if(addInput == null)
            {
                throw new NullImportPhoneDTOException("Parameter must be not null before it will be added to database");
            }

            Product product = new Product() { Name = addInput.Name, Designer = addInput.Designer, Cost = addInput.Cost, About = addInput.About };

            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Change(ChangePhoneDto changeInput)
        {
            if (changeInput == null)
            {
                throw new NullChangePhoneDTOException("Parameter must be not null before change");
            }

            Product product = db.Products.FirstOrDefault(x => x.Id == changeInput.Id);
            if (product == null)
            {
                throw new ProductNotExistException("Current product not exist");
            }

            product.Name = changeInput.Name;
            product.Designer = changeInput.Designer;
            product.Cost = changeInput.Cost;
            product.About = changeInput.About;

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new ProductNotExistException("Current product not exist");
            }

            db.Products.Remove(product);
            db.SaveChanges();
        }
    }

   

}
