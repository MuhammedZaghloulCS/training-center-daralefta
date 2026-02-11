using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IAssignUserSessionRepository
    {
        public Task AddRangeIfNotExistsAsync(List<UserSession> userSessions);


        public Task AddIfNotExistsAsync(UserSession userSession);


        public void Delete(UserSession userSession);




        public Task<List<Guid>> GetUsersIdsBySessionId(int sessionId);
        public Task<List<int>> GetSessionIdsByUserId(Guid userId);

    }
}
