using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services.Exceptions
{
    public class NullChangePhoneDTOException : ApplicationException
    {
        public NullChangePhoneDTOException(string message) : base(message)
        { }
    }
}
