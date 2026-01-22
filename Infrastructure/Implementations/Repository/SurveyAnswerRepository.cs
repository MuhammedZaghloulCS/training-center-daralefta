using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyAnswerRepository : GenericRepository<QuestionAnswer, int>, ISurveyAnswerRepository
    {
        public SurveyAnswerRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }
    
    }
}
