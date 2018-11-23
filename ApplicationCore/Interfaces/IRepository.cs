using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T: class
    {
         T GetById(int id);
         IEnumerable<T> ListAll();
         IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
         T Create(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}