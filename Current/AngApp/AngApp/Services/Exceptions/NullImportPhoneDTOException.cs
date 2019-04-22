using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngApp.Services.Exceptions
{
    public class NullImportPhoneDTOException : ApplicationException
    {
        public NullImportPhoneDTOException(string message) : base(message)
        { }
    }
}
