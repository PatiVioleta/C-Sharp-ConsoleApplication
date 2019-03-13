using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab10.Validation;
using Lab10.Domain;

namespace Lab10.Repository
{
    class AbstractRepo<ID, E> : IRepo<ID, E> where E : IHasID<ID>
    {
        public Dictionary<ID, E> Entities { get; set; }
        public IValidator<E> Validator { get; set; }

        public AbstractRepo(IValidator<E> v)
        {
            Entities = new Dictionary<ID, E>();
            Validator = v;
        }

        public E FindOne(ID id)
        {
            bool r = Entities.TryGetValue(id, out E e);
            return r ? e : default(E);
        }

        public virtual E Save(E entity)
        {
            Validator.Validate(entity);
            if (!Entities.TryGetValue(entity.Id, out E e))
            {
                Entities.Add(entity.Id, entity);
                return entity;
            }
            else
            {
                throw new MyException("Exista o entitate cu acest ID!\n");
            }
        }

        public virtual E Delete(ID id)
        {
            if (Entities.TryGetValue(id, out E e))
            {
                E aux = Entities[id];
                Entities.Remove(id);
                return aux;
            }
            else
                throw new MyException("Nu exista entitate cu acest ID!\n");
        }

        public virtual E Update(E entity)
        {
            Validator.Validate(entity);
            if (!Entities.TryGetValue(entity.Id, out E e))
            {
                throw new MyException("Nu exista entitate cu acest ID!\n");
            }
            else
            {
                return Entities[entity.Id] = entity;
            }
        }

        public IEnumerable<E> FindAll()
        {
            return Entities.Values;
        }

        public int Size()
        {
            return Entities.Count;
        }
    }
}
