using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services.Exceptions
{
    public class UserNameNullException : ApplicationException
    {
        public UserNameNullException(string message) : base(message)
        { }
    }
}
