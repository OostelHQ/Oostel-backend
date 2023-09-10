using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Repositories
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll(bool eager);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindandInclude(Expression<Func<T, bool>> expression, bool eager);
        Task<T> Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<int> Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);

        Task<int> Count(Expression<Func<T, bool>> expression);
    }
}
