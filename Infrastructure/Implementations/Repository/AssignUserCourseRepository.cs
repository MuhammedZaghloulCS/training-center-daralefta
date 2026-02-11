using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class AssignUserCourseRepository : IAssignUserCourseRepository
    {
        private readonly ApplicationContext _context;

        public AssignUserCourseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddRangeIfNotExistsAsync(List<UsersCourse> userCourses)
        {
            if (userCourses == null || !userCourses.Any())
                return;

            var userId = userCourses.First().UserId;
            var courseIds = userCourses.Select(x => x.CourseId).ToList();

            var existing = await _context.UsersCourses
                .Where(x => x.UserId == userId &&
                            courseIds.Contains(x.CourseId))
                .ToListAsync();

            var toAdd = userCourses
                .Where(x => !existing.Any(e =>
                    e.UserId == x.UserId &&
                    e.CourseId == x.CourseId))
                .ToList();

            if (toAdd.Any())
                await _context.UsersCourses.AddRangeAsync(toAdd);
        }

        public async Task AddIfNotExistsAsync(UsersCourse userCourse)
        {
            var exists = await _context.UsersCourses.AnyAsync(x =>
                x.UserId == userCourse.UserId &&
                x.CourseId == userCourse.CourseId);

            if (!exists)
                await _context.UsersCourses.AddAsync(userCourse);
        }

        public void Delete(UsersCourse userCourse)
        {
            _context.UsersCourses.Remove(userCourse);
        }

        public async Task<List<Guid>> GetUsersIdsByCourseId(int sessionId) => await _context.UsersCourses.Where(ut => ut.CourseId == sessionId)
               .Select(ut => ut.UserId)
               .ToListAsync();

        public async Task<List<int>> GetCourseIdsByUserId(Guid userId)=>await _context.UsersCourses.Where(ut => ut.UserId == userId)
                .Select(ut => ut.CourseId)
                .ToListAsync();
    }

}
