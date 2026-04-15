using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IUserTrainingRepository : IGenericRepository<UsersTrainings, int>

    {
        public Task<List<Schedule>> GetScheduleAsync(Expression<Func<UsersTrainings, bool>> predicate);
        public Task<List<Schedule>> GetLecturerScheduleAsync(Expression<Func<UserSession, bool>> predicate);

    }
}
