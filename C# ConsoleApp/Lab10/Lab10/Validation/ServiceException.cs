using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Validation
{
    class ServiceException : Exception
    {
        public ServiceException(string err) : base(err) { }
    }
}
