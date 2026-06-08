using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories.ISysRepositories
{
    public interface ISurveyUsersRepository
    {
        public Task AddUserToSurveyAsync(Guid userId, int surveyId);
        public Task MarkSurveyAsCompletedAsync(Guid userId, int surveyId);
        public Task<List<SurveyUsers>> GetAllSurveysForUserAsync(Guid userId, bool isCompleted, params Expression<Func<SurveyUsers, object>>[] includeProperties);
        public Task<List<SurveyUsers>> GetAllUsersForSurveyAsync(int surveyId, bool isCompleted, params Expression<Func<SurveyUsers, object>>[] includeProperties);
        public Task<List<SurveyUsers>> GetAllSpecifiedSurveysAsync(params Expression<Func<SurveyUsers, object>>[] includeProperties);

    }
}
