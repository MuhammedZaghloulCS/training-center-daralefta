using Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface ITrainingsSurveysRepository : IGenericRepository<TrainingsSurveys, int>
    {
        Task<List<TrainingsSurveys>> FindAsync(Expression<Func<TrainingsSurveys, bool>> predicate);
        Task AddRangeAsync(List<TrainingsSurveys> entities);
        void DeleteRange(List<TrainingsSurveys> entities);
    }
}
