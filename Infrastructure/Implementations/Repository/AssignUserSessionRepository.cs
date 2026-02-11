using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class AssignUserSessionRepository : IAssignUserSessionRepository
    {
        private readonly ApplicationContext _context;

        public AssignUserSessionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddRangeIfNotExistsAsync(List<UserSession> userSessions)
        {
            if (userSessions == null || !userSessions.Any())
                return;

            var userId = userSessions.First().UserId;
            var sessionIds = userSessions.Select(x => x.SessionId).ToList();

            var existing = await _context.UserSessions
                .Where(x => x.UserId == userId &&
                            sessionIds.Contains(x.SessionId))
                .ToListAsync();

            var toAdd = userSessions
                .Where(x => !existing.Any(e =>
                    e.UserId == x.UserId &&
                    e.SessionId == x.SessionId))
                .ToList();

            if (toAdd.Any())
                await _context.UserSessions.AddRangeAsync(toAdd);
        }

        public async Task AddIfNotExistsAsync(UserSession userSession)
        {
            var exists = await _context.UserSessions.AnyAsync(x =>
                x.UserId == userSession.UserId &&
                x.SessionId == userSession.SessionId);

            if (!exists)
                await _context.UserSessions.AddAsync(userSession);
        }

        public void Delete(UserSession userSession)
        {
            _context.UserSessions.Remove(userSession);
        }

        public async Task<List<Guid>> GetUsersIdsBySessionId(int sessionId)=> await _context.UserSessions.Where(ut => ut.SessionId == sessionId)
               .Select(ut => ut.UserId)
               .ToListAsync();
         
        public async Task<List<int>> GetSessionIdsByUserId(Guid userId)=> await _context.UserSessions.Where(ut => ut.UserId == userId)
                .Select(ut => ut.SessionId)
                .ToListAsync();
        
    }

}
