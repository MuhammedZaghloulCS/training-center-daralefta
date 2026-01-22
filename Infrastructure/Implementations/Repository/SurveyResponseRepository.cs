using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyResponseRepository : GenericRepository<SurveyResponse, int>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
