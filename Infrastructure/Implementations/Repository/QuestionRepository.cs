using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.Repository
{
    public class QuestionRepository : GenericRepository<Question, int>, IQuestionRepository
    {
        private readonly ApplicationContext _context;

        public QuestionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Question>> FindAsync(Expression<Func<Question, bool>> predicate)
        {
            return await _context.Set<Question>().Where(predicate).ToListAsync();
        }
    }
}
