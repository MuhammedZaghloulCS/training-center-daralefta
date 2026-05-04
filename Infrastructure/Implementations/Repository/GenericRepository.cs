using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected ApplicationContext context;
        protected DbSet<T> dbSet;
        public GenericRepository(ApplicationContext context )
        
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<List<T>> FindRowAsync(Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbSet.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (predicate != null)
                query = query.Where(predicate);


            return await query.ToListAsync();
        }
        public async Task<List<T>> NewFindRowAsync(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, bool acsending = true, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbSet.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
            {
                query = acsending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbSet.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.AsNoTracking().ToListAsync();

        }
        public async Task<(List<T> items, int totalCount)> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbSet.AsQueryable();

            // Apply includes
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // Apply filter
            if (predicate != null)
                query = query.Where(predicate);

            // Get total count before pagination
           
            // Apply ordering
            if (orderBy != null)
            {
                query = ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            // Apply pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, query.Count());
        }

        public Task<T?> GetByPkAsync(TKey PK,params Expression<Func<T, object>>[] includeProperties)
        {

            foreach (var item in includeProperties)
            {
                dbSet.Include(item);
            }
            return dbSet.FindAsync(PK).AsTask();
        }

        public void Update(T entity)
        {
            // Check if entity is already tracked to avoid double-tracking
            var entry = context.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Equals(entity));
            if (entry != null)
            {
                // Entity is already tracked, just mark as modified
                entry.State = EntityState.Modified;
            }
            else
            {
                // Entity is not tracked, attach and mark as modified
                dbSet.Update(entity);
            }
        }
        
        public void UpdateIfTracked(T entity)
        {
            Update(entity);
        }
        public async Task<T> GetFirstByPropAsync(Expression<Func<T, bool>> predicate)
        {
            var result=await dbSet.Where(predicate).FirstOrDefaultAsync();
            return result;
        }
        public void DeleteRange(List<T> entities)
        {
            dbSet.RemoveRange(entities);
        }


    }
}
