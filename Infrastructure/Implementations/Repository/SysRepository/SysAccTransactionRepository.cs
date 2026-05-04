using del.Models;
using Domain.Entities;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysAccTransactionRepository : ISysAccTransactionRepository
    {
        private readonly security_dbContext _security_dbContext;
        private readonly ApplicationContext _ApplicationContext;

        public SysAccTransactionRepository(security_dbContext context, ApplicationContext applicationContext)
        {
            _security_dbContext = context;
            _ApplicationContext = applicationContext;
        }

        public async Task<SessionAttendanceResponse> GetSessionAttendance(int sessionId)
        {
            var session = await _ApplicationContext.Session
                .Include(s => s.Room)
                .Include(s => s.Training)
                    .ThenInclude(t => t.UsersTrainings)
                        .ThenInclude(ut => ut.User)
                .Include(s => s.LecturerersSessions)
                    .ThenInclude(ls => ls.User)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                return null;

            // Check if session is in the future
            var sessionDateTime = session.SessionDate.Date + session.EndTime;
            var isFutureSession = sessionDateTime > DateTime.Now;

            // Get students and lecturers with full data
            var students = (session.Training?.UsersTrainings?
                .Where(ut => ut.User != null)
                .Select(ut => (ut.User.Id, ut.User.pin, ut.User.FirstName, ut.User.LastName, ut.User.Email, ut.User.PhoneNumber))
                .ToList()) ?? new List<(Guid, string, string, string, string, string)>();

            var lecturers = (session.LecturerersSessions?
                .Where(ls => ls.User != null)
                .Select(ls => (ls.User.Id, ls.User.pin, ls.User.FirstName, ls.User.LastName, ls.User.Email, ls.User.PhoneNumber))
                .ToList()) ?? new List<(Guid, string, string, string, string, string)>();

            // Debug: Log counts
            System.Diagnostics.Debug.WriteLine($"Students count: {students.Count}");
            System.Diagnostics.Debug.WriteLine($"Lecturers count: {lecturers.Count}");

            var studentPins = students.Select(s => s.Item2).Where(p => !string.IsNullOrEmpty(p)).ToList();
            var lecturerPins = lecturers.Select(l => l.Item2).Where(p => !string.IsNullOrEmpty(p)).ToList();

            var allPins = studentPins.Union(lecturerPins).Distinct().ToList();

            // Get all sessions in the same day for timing rules
            var allSessionsInDay = await _ApplicationContext.Session
                .Where(x => x.SessionDate == session.SessionDate)
                .OrderBy(x => x.StartTime)
                .ToListAsync();

            var currentSession = allSessionsInDay.First(x => x.Id == sessionId);

            var previousSession = allSessionsInDay
                .Where(x => x.StartTime < currentSession.StartTime)
                .OrderByDescending(x => x.StartTime)
                .FirstOrDefault();

            var nextSession = allSessionsInDay
                .Where(x => x.StartTime > currentSession.StartTime)
                .OrderBy(x => x.StartTime)
                .FirstOrDefault();

            var sessionStart = session.SessionDate.Date + currentSession.StartTime;
            var sessionEnd = session.SessionDate.Date + currentSession.EndTime;

            DateTime entryFrom = previousSession == null
                ? session.SessionDate.Date
                : session.SessionDate.Date + previousSession.EndTime;

            DateTime? exitTo = nextSession == null
                ? null
                : session.SessionDate.Date + nextSession.StartTime;

            // Get biometric logs
            var logs = await _security_dbContext.acc_transactions
                .Where(t => allPins.Contains(t.pin) && t.event_time.Date == session.SessionDate.Date)
                .OrderBy(t => t.pin)
                .ThenBy(t => t.event_time)
                .ToListAsync();

            var groupedLogs = logs.GroupBy(x => x.pin).ToDictionary(x => x.Key, x => x.ToList());

            // Build attendance for students
            var (presentStudents, absentStudents) = BuildAttendance(
                students.Select(s => (s.Item1, s.Item2, s.Item3, s.Item4, s.Item5, s.Item6)).ToList(),
                groupedLogs,
                sessionStart,
                sessionEnd,
                entryFrom,
                exitTo,
                session.Room?.AttRoomIdinside,
                session.Room?.AttRoomIdOutSide);

            // Build attendance for lecturers
            var (presentLecturers, absentLecturers) = BuildAttendance(
                lecturers.Select(l => (l.Item1, l.Item2, l.Item3, l.Item4, l.Item5, l.Item6)).ToList(),
                groupedLogs,
                sessionStart,
                sessionEnd,
                entryFrom,
                exitTo,
                session.Room?.AttRoomIdinside,
                session.Room?.AttRoomIdOutSide);

            return new SessionAttendanceResponse
            {
                SessionId = session.Id,
                SessionDate = session.SessionDate,
                Topic = session.Topic,
                IsFutureSession = isFutureSession,
                PresentStudents = presentStudents,
                AbsentStudents = absentStudents,
                PresentLecturers = presentLecturers,
                AbsentLecturers = absentLecturers
            };
        }

        private (List<AttendanceDto> present, List<AttendanceDto> absent) BuildAttendance(
            List<(Guid Id, string pin, string firstName, string lastName, string email, string phoneNumber)> users,
            Dictionary<string, List<acc_transaction>> groupedLogs,
            DateTime sessionStart,
            DateTime sessionEnd,
            DateTime entryFrom,
            DateTime? exitTo,
            string inId,
            string outId)
        {
            var present = new List<AttendanceDto>();
            var absent = new List<AttendanceDto>();

            foreach (var user in users)
            {
                var userLogs = !string.IsNullOrEmpty(user.pin) && groupedLogs.ContainsKey(user.pin)
                    ? groupedLogs[user.pin]
                    : new List<acc_transaction>();

                var firstEntry = userLogs
                    .Where(x => x.event_point_id == inId && x.event_time > entryFrom && x.event_time <= sessionEnd)
                    .OrderBy(x => x.event_time)
                    .Select(x => (DateTime?)x.event_time)
                    .FirstOrDefault();

                bool isAbsent = firstEntry == null;

                var lastExit = userLogs
                    .Where(x => x.event_point_id == outId && x.event_time > sessionEnd && (!exitTo.HasValue || x.event_time < exitTo.Value))
                    .OrderBy(x => x.event_time)
                    .Select(x => (DateTime?)x.event_time)
                    .FirstOrDefault();

                int durationMinutes = isAbsent
                    ? 0
                    : CalculateDuration(userLogs, sessionStart, sessionEnd, inId, outId);

                var dto = new AttendanceDto
                {
                    UserId = user.Id,
                    Pin = user.pin,
                    FullName = $"{user.firstName} {user.lastName}",
                    Email = user.email,
                    PhoneNumber = user.phoneNumber,
                    FirstEntry = isAbsent ? "غائب" : firstEntry.Value.ToString("HH:mm"),
                    LastExit = isAbsent ? "غائب" : lastExit?.ToString("HH:mm") ?? "غير محدد",
                    Duration = isAbsent ? "غائب" : TimeSpan.FromMinutes(durationMinutes).ToString(@"hh\:mm"),
                    IsPresent = !isAbsent
                };

                if (isAbsent)
                    absent.Add(dto);
                else
                    present.Add(dto);
            }

            return (present, absent);
        }

        private int CalculateDuration(
            List<acc_transaction> logs,
            DateTime sessionStart,
            DateTime sessionEnd,
            string inId,
            string outId)
        {
            DateTime? enteredAt = null;
            int total = 0;

            foreach (var log in logs)
            {
                if (log.event_point_id == inId)
                {
                    if (enteredAt == null)
                        enteredAt = log.event_time;
                }
                else if (log.event_point_id == outId)
                {
                    if (enteredAt != null)
                    {
                        var start = enteredAt.Value < sessionStart ? sessionStart : enteredAt.Value;
                        var end = log.event_time > sessionEnd ? sessionEnd : log.event_time;

                        if (end > start)
                            total += (int)(end - start).TotalMinutes;

                        enteredAt = null;
                    }
                }
            }

            if (enteredAt != null)
            {
                var start = enteredAt.Value < sessionStart ? sessionStart : enteredAt.Value;
                if (sessionEnd > start)
                    total += (int)(sessionEnd - start).TotalMinutes;
            }

            return total;
        }
    }
}
