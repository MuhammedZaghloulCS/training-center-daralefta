using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.Repository
{
    public class QuestionAnswerRepository : GenericRepository<QuestionAnswer, int>, IQuestionAnswerRepository
    {
        private readonly ApplicationContext _context;

        public QuestionAnswerRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<QuestionAnswer>> FindAsync(Expression<Func<QuestionAnswer, bool>> predicate)
        {
            return await _context.Set<QuestionAnswer>().Where(predicate).ToListAsync();
        }

        public async Task AddRangeAsync(List<QuestionAnswer> entities)
        {
            await _context.Set<QuestionAnswer>().AddRangeAsync(entities);
        }
    }
}
