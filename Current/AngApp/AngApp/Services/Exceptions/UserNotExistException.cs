using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services.Exceptions
{
    public class UserNotExistException : ApplicationException
    {
        public UserNotExistException(string message) : base(message)
        { }
    }
}
