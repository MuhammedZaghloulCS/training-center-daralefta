using del.Models;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class GenericSysRepository<T> : IGenericSysRepository<T> where T : class
    {
        private readonly  security_dbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericSysRepository(security_dbContext context)
        {
            this._context = context;
            _dbSet=_context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>predicate=null)
        {
            IQueryable<T> query=_dbSet.AsNoTracking();
            if(predicate is not null)
                query=query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetFirstOrderedByAsync<TKey>(Expression<Func<T, TKey>> order = null, bool descending = true)
        {
            var query = _dbSet.AsQueryable();
            if (order != null)
            {
                query = descending ? query.OrderByDescending(order) : query.OrderBy(order);
            }
           return await query.FirstOrDefaultAsync();
             
        }

        public async Task<List<T>> GetPaginatedAsync<TKey>(int pageNumber, int pageSize, Expression<Func<T, TKey>> order, bool descending = true)
        {
            var query = _dbSet.AsQueryable();
            if (order != null)
            {
                query = descending ? query.OrderByDescending(order) : query.OrderBy(order);
            }
            return await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task<T?> GetByPropAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<List<T>> GetAllByPropAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task AddRangeAsync(List<T> users)
        {
            await _dbSet.AddRangeAsync(users);
        }

        public  void RemoveRange(List<T> entities)
        {
             _dbSet.RemoveRange(entities);
        }
    }
}
