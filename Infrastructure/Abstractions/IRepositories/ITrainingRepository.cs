using Domain.Entities;
using Infrastructure.Implementations.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface ITrainingRepository: IGenericRepository<Training, int>
    {
        public Training? GetTrainingWithAllCoursesAsync(int trainingId);

        public Task<List<Training>> GetAllTrainingsWithAllCoursesAsync(Expression<Func<Training, bool>> filter = null);
        public Task<(List<Training>, int totalNumber)> GetTrainingsWithAllCoursesPagedAsync(int pageSize = 10, int pageNumber = 1, Expression<Func<Training, bool>> filter = null);

        public Task<(List<Training>, int totalNumber)> GetTrainingsWithAllCoursesAndSessionsAndLecturersAndStudentsPagedAsync( int pageNumber = 1,int pageSize = 10, Expression<Func<Training, bool>> filter = null, Expression<Func<Training, object>> orderBy = null, bool acsending = true);
        public Task<Training> GetTrainingWithUsersTrainingsById(int id);

        public Task<Training?> GetTrainingWithSessionsById(int trainingId, params Expression<Func<Training, object>>[] includes);
        public Task<(List<TrainingPagedDto>, int totalNumber)> GetTrainingsPagedProjectedAsync(
    int pageNumber = 1,
    int pageSize = 10,
    Expression<Func<Training, bool>> filter = null);

    }
}
