using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.Repository
{
    public class TrainingsSurveysRepository : GenericRepository<TrainingsSurveys, int>, ITrainingsSurveysRepository
    {
        private readonly ApplicationContext _context;

        public TrainingsSurveysRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TrainingsSurveys>> FindAsync(Expression<Func<TrainingsSurveys, bool>> predicate)
        {
            return await _context.Set<TrainingsSurveys>().Where(predicate).ToListAsync();
        }

        public async Task AddRangeAsync(List<TrainingsSurveys> entities)
        {
            await _context.Set<TrainingsSurveys>().AddRangeAsync(entities);
        }

        public void DeleteRange(List<TrainingsSurveys> entities)
        {
            _context.Set<TrainingsSurveys>().RemoveRange(entities);
        }
    }
}
