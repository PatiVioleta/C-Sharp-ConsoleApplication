using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10.Repository
{
    public interface IRepo<ID, E>
    {
        E FindOne(ID id);
        IEnumerable<E> FindAll();
        E Save(E entity);
        E Delete(ID id);
        E Update(E entity);
    }
}
