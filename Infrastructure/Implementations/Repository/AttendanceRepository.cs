using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.Repository
{
    public class AttendanceRepository : GenericRepository<Attendance, int>, IAttendanceRepository
    {
        private readonly ApplicationContext _context;

        public AttendanceRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAttendanceBySessionIdAsync(int sessionId)
        {
            return await _context.Attendances
                .Include(a => a.User)
                .Include(a => a.Session)
                .Where(a => a.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<Attendance?> GetAttendanceByUserAndSessionAsync(Guid userId, int sessionId)
        {
            return await _context.Attendances
                .Include(a => a.User)
                .Include(a => a.Session)
                .FirstOrDefaultAsync(a => a.UserId == userId && a.SessionId == sessionId);
        }

        public async Task<List<Attendance>> GetAttendanceByUserAndDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Attendances
                .Include(a => a.User)
                .Include(a => a.Session)
                .Where(a => a.UserId == userId &&
                            a.Session.SessionDate >= startDate &&
                            a.Session.SessionDate <= endDate)
                .ToListAsync();
        }

        public async Task<bool> AttendanceExistsAsync(Guid userId, int sessionId)
        {
            return await _context.Attendances
                .AnyAsync(a => a.UserId == userId && a.SessionId == sessionId);
        }
    }
}
