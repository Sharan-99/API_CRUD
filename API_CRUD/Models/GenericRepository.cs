using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_CRUD.Models
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly APICRUD_DBContext context;
        public GenericRepository(APICRUD_DBContext context)
        {
            this.context = context;
        }

        public T Add(T item)
        {
            return context.Add(item).Entity;
        }

        public T Delete(T item)
        {
            return context.Remove(item).Entity;
        }

        public IReadOnlyList<T> Get(Expression<Func<T, bool>> condition = null)
        {
            var entities = context.Set<T>();
            if (condition != null)
                return entities.Where(condition).ToList();
            return entities.ToList();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public T Update(T item)
        {
            return context.Update(item).Entity;
        }
    }
}
