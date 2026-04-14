using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
namespace Infrastructure.Implementations.Repository
{
    public class TrainingRepository : GenericRepository<Domain.Entities.Training, int>, Infrastructure.Abstractions.IRepositories.ITrainingRepository
    {
        private readonly ApplicationContext _context;
        public TrainingRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Training>> GetAllTrainingsWithAllCoursesAsync(Expression<Func<Training, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _context.Training.Where(t => t.IsDeleted == false).Where(filter).Include(t => t.CoursesTrainings).ThenInclude(ct => ct.Course).ToListAsync();
            }
            return await _context.Training.Where(t => t.IsDeleted == false).Include(t => t.CoursesTrainings).ThenInclude(ct => ct.Course).ToListAsync();
        }

        public Training? GetTrainingWithAllCoursesAsync(int trainingId)
        {

            return _context.Training.Where(t => t.IsDeleted == false).Include(t => t.CoursesTrainings).ThenInclude(ct => ct.Course).FirstOrDefault(t => t.Id == trainingId);

        }

        public async Task<(List<Training>, int totalNumber)> GetTrainingsWithAllCoursesPagedAsync(int pageSize = 10, int pageNumber = 1, Expression<Func<Training, bool>> filter = null)
        {
            var query = _context.Training.Where(t => t.IsDeleted == false)
            .AsNoTracking()
            .Include(t => t.CoursesTrainings)
            .ThenInclude(ct => ct.Course).AsQueryable();

            if (filter != null)
                query = query.Where(filter);
            var totalCount = await query.CountAsync();
            var data = await query.OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<(List<Training>, int totalNumber)>
        GetTrainingsWithAllCoursesAndSessionsAndLecturersAndStudentsPagedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            Expression<Func<Training, bool>> filter = null, Expression<Func<Training, object>> orderBy = null, bool acsending = true)
        {
            var baseQuery = _context.Training
                .Where(t => !t.IsDeleted)
                .AsNoTracking();

            if (filter != null)
                baseQuery = baseQuery.Where(filter);

            var totalCount = await baseQuery.CountAsync();
            if (orderBy != null)
            {
                baseQuery = acsending ? baseQuery.OrderBy(orderBy) : baseQuery.OrderByDescending(orderBy);
                var data = await baseQuery

               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .AsSplitQuery()
               .Include(t => t.CoursesTrainings)

               .Include(t => t.Sessions)
                   .ThenInclude(s => s.LecturerersSessions)
                       .ThenInclude(ls => ls.User)
               .Include(t => t.Sessions)
                   .ThenInclude(s => s.Course)

               .Include(t => t.UsersTrainings)
                   .ThenInclude(ut => ut.User)
               .ToListAsync();

                return (data, totalCount);
            }
            else
            {
                var data = await baseQuery
                    .OrderBy(t => t.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsSplitQuery()
                    .Include(t => t.CoursesTrainings)

                    .Include(t => t.Sessions)
                        .ThenInclude(s => s.LecturerersSessions)
                            .ThenInclude(ls => ls.User)
                    .Include(t => t.Sessions)
                        .ThenInclude(s => s.Course)

                    .Include(t => t.UsersTrainings)
                        .ThenInclude(ut => ut.User)
                    .ToListAsync();

                return (data, totalCount);
            }
        }
        public async Task<Training?> GetTrainingWithUsersTrainingsById(int id)
        {
            return await _context.Training
                .Include(t => t.UsersTrainings)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<Training?> GetTrainingWithSessionsById(int trainingId, params Expression<Func<Training, object>>[] includes)
        {
            var query = _context.Training.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(t => t.Id == trainingId && !t.IsDeleted);

        }
    }

}
