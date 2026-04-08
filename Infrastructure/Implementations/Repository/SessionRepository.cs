using Infrastructure.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SessionRepository : GenericRepository<Domain.Entities.Session, int>, ISessionRepository
    {
        private readonly Infrastructure.Context.ApplicationContext _context;
        public SessionRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Guid>> GetUsersIdsFromTrainingforSessionsBySessionIdAsync(int sessionId)
        {
            var session = await _context.Session
                .Include(s => s.Training)
                .ThenInclude(t => t.UsersTrainings)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            var usersIds = session?.Training?.UsersTrainings
                .Select(ut => ut.UserId)
                .ToList();
            
            if (usersIds == null)
                return new List<Guid>();
      
            return usersIds;
        }


    }
}
