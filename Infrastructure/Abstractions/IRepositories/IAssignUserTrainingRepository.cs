using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IAssignUserTrainingRepository
    {
        public Task AddRangeIfNotExistsAsync(List<UsersTrainings> userTrainings);

        public Task AddIfNotExistsAsync(UsersTrainings userTraining);


        public void Delete(UsersTrainings userTraining);
        public Task<List<Guid>> GetUsersIdsByTrainingId(int trainingId);
        public Task<List<int>> GetTrainingIdsByUserId(Guid userId);

        //public Task<UsersTrainings> GetByPkAsync(Guid trainingId, Func<IQueryable<UsersTrainings>, IQueryable<UsersTrainings>> include = null);
    }
}
