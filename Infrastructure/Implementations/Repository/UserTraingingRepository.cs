using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
namespace Infrastructure.Implementations.Repository
{
    public class UserTrainingRepository : GenericRepository<Domain.Entities.UsersTrainings, int>, Abstractions.IRepositories.IUserTrainingRepository
    {
        private readonly ApplicationContext _context;

        public UserTrainingRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetScheduleAsync(Expression<Func<UsersTrainings, bool>> predicate)
        {
            var today = DateTime.Today;

            // Get user's trainings
            var userTrainings = await _context.UsersTrainings
                .Where(predicate)
                .Select(ut => new { ut.UserId, ut.TrainingId })
                .ToListAsync();

            if (!userTrainings.Any())
                return new List<Schedule>();

            var userId = userTrainings.First().UserId;
            var trainingIds = userTrainings.Select(ut => ut.TrainingId).ToList();

            // Get sessions where user is an instructor in their trainings
            var instructorSessionIds = await _context.UserSessions
                .Where(us => us.UserId == userId)
                .Where(us => trainingIds.Contains(us.Session.TrainingId.Value))
                .Where(us => us.Session.SessionDate >= today)
                .Select(us => us.SessionId)
                .ToListAsync();

            var sessions = await _context.UsersTrainings
                 .Where(predicate)
                 .SelectMany(ut => ut.Training.Sessions)
                 .Where(s => s.SessionDate >= today)
                 .Where(s => !instructorSessionIds.Contains(s.Id)) // Exclude sessions where user is instructor
                 .Select(s => new
                 {
                     Time = $"{s.StartTime.ToArabic12Hour()} - {s.EndTime.ToArabic12Hour()}",
                     Course = s.Course.Name,
                     Lecturers = s.LecturerersSessions
                                     .Select(ls => ls.User.FullName)
                                     .ToList(),
                     Building = s.Room.Building.Name,
                     Room = s.Room.Name,
                     Date = s.SessionDate,
                     SessionId = s.Id
                 }).GroupBy(s => s.Date)
                 .ToListAsync();
            List<Schedule> schedules = new List<Schedule>();
            foreach (var session in sessions)
            {
                schedules.Add(new Schedule
                {
                    Day = session.Key.ToString("dddd", new CultureInfo("ar-EG")),
                    Date = session.Key.ToString("yyyy-MM-dd"),
                    Lectures = session.Select(s => new Lecture
                    {
                        Time = s.Time,
                        Course = s.Course,
                        Lecturers = s.Lecturers,
                        Building = s.Building,
                        Room = s.Room,
                        SessionId = s.SessionId
                    }).ToList()
                });

            }
                
 

            return schedules;


        }
        public async Task<List<Schedule>> GetLecturerScheduleAsync(Expression<Func<UserSession, bool>> predicate)
        {
            var today = DateTime.Today;
            var sessions = await _context.UserSessions
                 .Where(predicate)
                 .Where(s => s.Session.SessionDate >= today)
                 .Select(s => new
                 {
                     Training = s.Session.Training.Title,
                    Time = $"{s.Session.StartTime.ToArabic12Hour()} - {s.Session.EndTime.ToArabic12Hour()}",
                     Course = s.Session.Course.Name,
                     Building = s.Session.Room.Building.Name,
                     Room = s.Session.Room.Name,
                     Date = s.Session.SessionDate,
                     SessionId = s.Session.Id
                 }).GroupBy(s => new { s.Date ,s.Training})
                 .ToListAsync();




            List<Schedule> schedules = new List<Schedule>();
            foreach (var session in sessions)
            {
                schedules.Add(new Schedule
                {
                    Day = session.Key.Date.ToString("dddd", new CultureInfo("ar-EG")),
                    Date = session.Key.Date.ToString("yyyy-MM-dd"),
                    Lectures = session.Select(s => new Lecture
                    {
                        Lecturers = new List<string> { session.Key.Training },
                        Time = s.Time,
                        Course = s.Course,
                        Building = s.Building,
                        Room = s.Room,
                        SessionId = s.SessionId
                    }).ToList()
                });

            }
                
 

            return schedules;


        }

   
    }
    public static class TimeExtensions
    {
        public static string ToArabic12Hour(this TimeSpan time)
        {
            return DateTime.Today.Add(time)
                .ToString("hh:mm tt")
                .Replace("AM", "ص")
                .Replace("PM", "م");
        }
    }
}