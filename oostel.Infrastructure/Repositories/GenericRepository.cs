﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oostel.Infrastructure.Data;
using Oostel.Common.Types;

namespace Oostel.Infrastructure.Repositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);

            return entity;
        }


        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression) =>
            _dbContext.Set<T>()
            .Where(expression);


        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            SaveChanges();

        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> FindandInclude(Expression<Func<T, bool>> expression, bool eager)
        {
            var query = _dbContext.Set<T>().Where(expression);
            if (eager)
            {
                var navigations = _dbContext.Model.FindEntityType(typeof(T))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                    query = query.Include(property.Name);
            }
            return query;
        }

        public async Task<IEnumerable<T>> GetAll(bool eager)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (eager)
            {
                var navigations = _dbContext.Model.FindEntityType(typeof(T))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                    query = query.Include(property.Name);
            }
            return query;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> Remove(T entity)
        {
            _dbSet.Remove(entity);
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return 1;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            SaveChanges();

        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }


    }
}
