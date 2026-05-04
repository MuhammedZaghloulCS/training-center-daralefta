using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IAttendanceRepository : IGenericRepository<Attendance, int>
    {
        /// <summary>
        /// Gets attendance records for a specific session
        /// </summary>
        Task<List<Attendance>> GetAttendanceBySessionIdAsync(int sessionId);

        /// <summary>
        /// Gets attendance record for a specific user in a specific session
        /// </summary>
        Task<Attendance?> GetAttendanceByUserAndSessionAsync(Guid userId, int sessionId);

        /// <summary>
        /// Gets attendance records for a specific user in a date range
        /// </summary>
        Task<List<Attendance>> GetAttendanceByUserAndDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Checks if attendance exists for a user in a session
        /// </summary>
        Task<bool> AttendanceExistsAsync(Guid userId, int sessionId);
    }
}
