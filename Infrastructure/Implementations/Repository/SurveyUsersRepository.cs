using Domain.Entities;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyUsersRepository : ISurveyUsersRepository
    {
        private readonly ApplicationContext _context;
        public SurveyUsersRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddUserToSurveyAsync(Guid userId, int surveyId)
        {
            var result=await _context.SurveysUsers.AddAsync(new SurveyUsers
            {
                UserId = userId,
                SurveyId = surveyId,
            });
            
        }

        public Task<List<SurveyUsers>> GetAllSpecifiedSurveysAsync(Expression<Func<SurveyUsers,bool>>filter=null, params Expression<Func<SurveyUsers, object>>[] includeProperties)
        {
            var query = _context.SurveysUsers.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToListAsync();
        }



        public Task<List<SurveyUsers>> GetAllSurveysForUserAsync(Guid userId, bool isCompleted=false, params Expression<Func<SurveyUsers, object>>[] includeProperties)
        {
            var query = _context.SurveysUsers.Where(su => su.UserId == userId).AsQueryable();

          
                query = query.Where(s=>isCompleted==isCompleted);
            
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToListAsync();
        }

        public Task<List<SurveyUsers>> GetAllUsersForSurveyAsync(int surveyId, bool isCompleted, params Expression<Func<SurveyUsers, object>>[] includeProperties)
        {
            var query = _context.SurveysUsers.Where(su => su.SurveyId == surveyId).AsQueryable();
            query = query.Where(s => isCompleted == isCompleted);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToListAsync();
        }

        public async Task MarkSurveyAsCompletedAsync(Guid userId, int surveyId)
        {
             await _context.SurveysUsers.Where(su => su.UserId == userId && su.SurveyId == surveyId)
                .ExecuteUpdateAsync(su => su.SetProperty(s => s.HasCompleted, true));
        }
    }
}
