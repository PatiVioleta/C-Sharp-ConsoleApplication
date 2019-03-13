using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Validation
{
    public interface IValidator<E>
    {
        void Validate(E e);
    }
}
