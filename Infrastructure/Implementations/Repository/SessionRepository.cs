using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class SessionRepository : GenericRepository<Domain.Entities.Session, int>, Infrastructure.Abstractions.IRepositories.ISessionRepository
    {
        public SessionRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }
    
    
    }
}
