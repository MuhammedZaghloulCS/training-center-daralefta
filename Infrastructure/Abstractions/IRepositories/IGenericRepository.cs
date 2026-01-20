using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetByPkAsync(TKey PK);
        Task<List<T>> FindRowAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<(List<T> items, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, bool ascending = true, params Expression<Func<T, object>>[] includeProperties);
    }
}
