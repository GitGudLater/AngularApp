using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services.Exceptions
{
    public class ProductNotExistException : ApplicationException
    {
        public ProductNotExistException(string message) : base(message)
        { }
    }
}
