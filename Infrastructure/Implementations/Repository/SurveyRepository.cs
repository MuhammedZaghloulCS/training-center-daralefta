using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SurveyRepository : GenericRepository<Domain.Entities.Survey, int>, Infrastructure.Abstractions.IRepositories.ISurveyRepository
    {
        private readonly Infrastructure.Context.ApplicationContext _context;
        public SurveyRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Survey> GetSurveyWithQuestion(int id) {

            var result =await _context.Survey.Include(s => s.Questions).Include(s=>s.SurveyUsers).ThenInclude(s=>s.User).FirstOrDefaultAsync(s => s.Id == id);

            return result;

        }



    }
}
