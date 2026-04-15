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
            
            var sessions = await _context.UsersTrainings
                 .Where(predicate)
                 .SelectMany(ut => ut.Training.Sessions)
                 .Select(s => new
                 {
                     Time = $"{s.StartTime.ToArabic12Hour()} - {s.EndTime.ToArabic12Hour()}",
                     Course = s.Course.Name,
                     Lecturers = s.LecturerersSessions
                                     .Select(ls => ls.User.FullName)
                                     .ToList(),
                     Building = s.Room.Building.Name,
                     Room = s.Room.Name,
                     Date = s.SessionDate
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
                        Room = s.Room
                    }).ToList()
                });

            }
                
 

            return schedules;


        }
        public async Task<List<Schedule>> GetLecturerScheduleAsync(Expression<Func<UserSession, bool>> predicate)
        {
            var sessions = await _context.UserSessions
                 .Where(predicate)
                 
                 .Select(s => new
                 {
                     Training = s.Session.Training.Title,
                    Time = $"{s.Session.StartTime.ToArabic12Hour()} - {s.Session.EndTime.ToArabic12Hour()}",
                     Course = s.Session.Course.Name,
                     Building = s.Session.Room.Building.Name,
                     Room = s.Session.Room.Name,
                     Date = s.Session.SessionDate
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
                        Lecturers = [session.Key.Training],
                        Time = s.Time,
                        Course = s.Course,
                        Building = s.Building,
                        Room = s.Room
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