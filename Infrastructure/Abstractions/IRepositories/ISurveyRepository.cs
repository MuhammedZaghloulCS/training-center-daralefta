using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface ISurveyRepository : IGenericRepository<Survey, int>
    {
        public Task<Survey> GetSurveyWithQuestion(int id);
    }
}