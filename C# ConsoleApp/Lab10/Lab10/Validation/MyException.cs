using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Validation
{
    class MyException : Exception
    {
        public MyException(string err) : base(err) { }
    }
}
