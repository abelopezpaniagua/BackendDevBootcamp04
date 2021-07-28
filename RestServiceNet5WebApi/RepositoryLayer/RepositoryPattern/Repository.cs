using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private DbSet<T> entities;

        public Repository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            entities = _applicationDBContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(int Id)
        {
            return entities.SingleOrDefault(c => c.Id == Id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");

            entities.Add(entity);
            _applicationDBContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");

            entities.Update(entity);
            _applicationDBContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");

            entities.Remove(entity);
            _applicationDBContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentException("entity");

            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _applicationDBContext.SaveChanges();
        }
    }
}
