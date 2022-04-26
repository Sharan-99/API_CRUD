using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_CRUD.Models
{
    public interface IRepository<T> where T:class
    {
        T Add(T item);
        T Update(T item);
        T Delete(T item);
        IReadOnlyList<T> Get(Expression<Func<T,bool>> condition=null);
        Task<int> SaveAsync();
    }
}
