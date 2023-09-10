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
        IEnumerable<T> GetAll(bool eager);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindandInclude(Expression<Func<T, bool>> expression, bool eager);
        Task<T> Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Task Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        public void Update(T entity);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
