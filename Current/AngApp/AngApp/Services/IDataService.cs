using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services
{
    public interface IPhonesCatalog
    {
        IEnumerable<PhoneDto> GetFullCatalog(string userName);

        IEnumerable<PhoneDto> GetFavoriteList(string userName);

        void ToggleFavoriteFlag(int id, string username);

        void Add(AddPhoneDto viewproduct);

        void Change(ChangePhoneDto viewproduct);

        void Delete(int id);
    }
}
