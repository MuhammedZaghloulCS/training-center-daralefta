using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IUsersTrainingsRepository
    {
        Task AddAsync(UsersTrainings entity);
        Task RemoveAsync(Guid userId, int trainingId);
        Task<bool> ExistsAsync(Guid userId, int trainingId);
        Task<List<ApplicationUser>> GetUsersByTrainingIdAsync(int trainingId);
    }

}
