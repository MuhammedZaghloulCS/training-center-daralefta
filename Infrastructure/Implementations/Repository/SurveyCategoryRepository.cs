using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyCategoryRepository : GenericRepository<SurveyCategory, int>, ISurveyCategoryRepository
    {
        public SurveyCategoryRepository(ApplicationContext context) : base(context)
        {
        }
    
    }
}
