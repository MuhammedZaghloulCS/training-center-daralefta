using Domain.Entities;
using Domain.Enums;
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
                .Where(t => !t.IsDeleted);

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

        public async Task<(List<TrainingPagedDto>, int totalNumber)> GetTrainingsPagedProjectedAsync(
    int pageNumber = 1,
    int pageSize = 10,
    Expression<Func<Training, bool>> filter = null)
        {
            var query = _context.Training
                .Where(t => !t.IsDeleted)
                .AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TrainingPagedDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,

                    Sessions = t.Sessions.Select(s => new SessionSummaryDto
                    {
                        Id = s.Id,
                        SessionDate = s.SessionDate,
                        StartTime = s.StartTime.ToString(),
                        EndTime = s.EndTime.ToString(),
                        Topic = s.Topic,
                        RoomId = s.RoomId,
                        CourseId = s.CourseId,
                        Course = s.Course == null ? null : new CourseSummaryDto
                        {
                            Id = s.Course.Id,
                            Name = s.Course.Name,
                            Description = s.Course.Description,
                            Duration = s.Course.Duration
                        },
                        LecturerersSessions = s.LecturerersSessions.Select(ls => new LecturerSessionSummaryDto
                        {
                            UserId = ls.UserId.ToString(),
                            User = ls.User == null ? null : new UserSummaryDto
                            {
                                Id = ls.User.Id.ToString(),
                                FullName = ls.User.FirstName + " " + ls.User.LastName,
                                Email = ls.User.Email,
                                PhoneNumber = ls.User.PhoneNumber
                            }
                        }).ToList()
                    }).ToList(),

                    CoursesTrainings = t.CoursesTrainings.Select(ct => new CourseTrainingSummaryDto
                    {
                        CourseId = ct.CourseId,
                        Course = ct.Course == null ? null : new CourseSummaryDto
                        {
                            Id = ct.Course.Id,
                            Name = ct.Course.Name,
                            Description = ct.Course.Description,
                            Duration = ct.Course.Duration
                        }
                    }).ToList(),

                    UsersTrainings = t.UsersTrainings.Select(ut => new UserTrainingSummaryDto
                    {
                        UserId = ut.UserId.ToString(),
                        User = ut.User == null ? null : new UserSummaryDto
                        {
                            Id = ut.User.Id.ToString(),
                            FullName = ut.User.FirstName + " " + ut.User.LastName,
                            Email = ut.User.Email,
                            PhoneNumber = ut.User.PhoneNumber
                        }
                    }).ToList(),

                    TrainingsSurveys = t.TrainingsSurveys.Select(ts => new TrainingSurveySummaryDto
                    {
                        SurveyId = ts.surveyId.Value,
                        Survey = ts.Survey == null || !ts.Survey.IsActive ? null : new SurveySummaryDto
                        {
                            Id = ts.Survey.Id,
                            Name = ts.Survey.Name,
                            Description = ts.Survey.Description,
                            IsActive = ts.Survey.IsActive,
                            Questions = ts.Survey.Questions.Select(q => new QuestionSummaryDto
                            {
                                Id = q.Id,
                                QuestionText = q.QuestionText,
                                QuestionType = q.QuestionType,
                                Options = q.Options,
                                IsRequired = q.IsRequired,
                                SortOrder = q.SortOrder
                            }).ToList()
                        }
                    }).ToList()
                })
                .ToListAsync();

            return (data, totalCount);
        }
    }
    public class TrainingPagedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SessionSummaryDto> Sessions { get; set; } = new();
        public List<CourseTrainingSummaryDto> CoursesTrainings { get; set; } = new();
        public List<UserTrainingSummaryDto> UsersTrainings { get; set; } = new();
        public List<TrainingSurveySummaryDto> TrainingsSurveys { get; set; } = new();
    }

    public class SessionSummaryDto
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Topic { get; set; }
        public int? RoomId { get; set; }
        public int? CourseId { get; set; }
        public CourseSummaryDto Course { get; set; }
        public List<LecturerSessionSummaryDto> LecturerersSessions { get; set; } = new();
    }

    public class LecturerSessionSummaryDto
    {
        public string UserId { get; set; }
        public UserSummaryDto User { get; set; }
    }

    public class UserSummaryDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CourseSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }

    public class CourseTrainingSummaryDto
    {
        public int CourseId { get; set; }
        public CourseSummaryDto Course { get; set; }
    }

    public class UserTrainingSummaryDto
    {
        public string UserId { get; set; }
        public UserSummaryDto User { get; set; }
    }

    public class TrainingSurveySummaryDto
    {
        public int SurveyId { get; set; }
        public SurveySummaryDto Survey { get; set; }
    }

    public class SurveySummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<QuestionSummaryDto> Questions { get; set; } = new();
    }

    public class QuestionSummaryDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public string Options { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }
    }
}
