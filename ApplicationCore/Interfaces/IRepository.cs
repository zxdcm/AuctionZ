using System;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T: class
    {
         T GetById(int id);
         IEnumerable<T> ListAll();
         IEnumerable<T> Find(Func<T, bool> predicate);
         T Create(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}