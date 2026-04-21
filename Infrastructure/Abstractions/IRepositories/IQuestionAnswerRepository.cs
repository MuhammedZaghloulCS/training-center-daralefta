using Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IQuestionAnswerRepository : IGenericRepository<QuestionAnswer, int>
    {
        Task<List<QuestionAnswer>> FindAsync(Expression<Func<QuestionAnswer, bool>> predicate);
        Task AddRangeAsync(List<QuestionAnswer> entities);
    }
}
