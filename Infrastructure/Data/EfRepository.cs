using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EfRepository<T>: IRepository<T> where T: class
    {
        protected readonly AuctionContext _context;
        protected void Save() => _context.SaveChanges();


        public EfRepository(AuctionContext dbContext)
        {
            _context = dbContext;
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            Save();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            Save();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> ListAll()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
