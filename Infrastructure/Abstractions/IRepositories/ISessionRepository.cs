using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface ISessionRepository: IGenericRepository<Session, int>
    {
        public Task<List<Guid>> GetUsersIdsFromTrainingforSessionsBySessionIdAsync(int sessionId);
        public Task<Session?> GetSessionById(int sessionId, params Expression<Func<Session, object>>[] includes);

    }
}
