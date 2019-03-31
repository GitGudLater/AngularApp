using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalApp.Models;

namespace PracticalApp.Services
{
    public interface IDataService
    {

        IEnumerable<FullList> GetPhones(string username, bool personal);

        void Toggle(int id, string username);

        void Set(FullList viewproduct);

        void Change(FullList viewproduct);

        void Delete(int id);
    }
}
