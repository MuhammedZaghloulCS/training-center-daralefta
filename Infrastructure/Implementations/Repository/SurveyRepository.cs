using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyRepository : GenericRepository<Domain.Entities.Survey, int>, Infrastructure.Abstractions.IRepositories.ISurveyRepository
    {
        public SurveyRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }
    
    
    }
}
