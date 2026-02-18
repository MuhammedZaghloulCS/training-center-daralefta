using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories.ISysRepositories
{
    public interface IGenericSysRepository<T> where T : class
    {
        public Task AddAsync(T entity);
        public void Delete(T entity);
        public void Update(T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetFirstOrderedByAsync<TKey>(Expression<Func<T, TKey>> order = null,bool descending=true);
        public Task<List<T>> GetPaginatedAsync<TKey>(int pageNumber, int pageSize, Expression<Func<T, TKey>> order, bool descending = true);
        public Task<T> GetByIdAsync(string id);

        public Task<T?> GetByPropAsync(Expression<Func<T, bool>> predicate);
    }
}
