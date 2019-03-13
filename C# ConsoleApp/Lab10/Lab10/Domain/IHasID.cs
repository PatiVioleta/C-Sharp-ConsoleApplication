using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Domain
{
    interface IHasID<ID>
    {
        ID Id { get; set; }
    }
}
