using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyQuestionRepository : GenericRepository<Domain.Entities.SurveyQuestion, int>, Infrastructure.Abstractions.IRepositories.ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }
    
    }
}
