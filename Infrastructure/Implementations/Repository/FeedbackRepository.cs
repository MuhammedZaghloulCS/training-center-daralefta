using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class FeedbackRepository : GenericRepository<Domain.Entities.Feedback, int>, IFeedbackRepository
    {
        private readonly Infrastructure.Context.ApplicationContext _context;
        public FeedbackRepository(ApplicationContext context) :base(context)
        {
            _context = context;
        }

    }
}
