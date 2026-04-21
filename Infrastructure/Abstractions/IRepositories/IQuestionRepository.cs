using Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IQuestionRepository : IGenericRepository<Question, int>
    {
        Task<List<Question>> FindAsync(Expression<Func<Question, bool>> predicate);
    }
}
