using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngApp.ViewModels.ProductModels;

namespace AngApp.Services
{
    public interface IPhonesCatalogService
    {
        IEnumerable<PhoneDto> GetFullCatalog(string userName);

        IEnumerable<PhoneDto> GetFavoriteList(string userName);

        void ToggleFavoriteFlag(int id, string userName);

        void Add(AddPhoneDto addInput);

        void Change(ChangePhoneDto changeInput);

        void Delete(int id);
    }
}
