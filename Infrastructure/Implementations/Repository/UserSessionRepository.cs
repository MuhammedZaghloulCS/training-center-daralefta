using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class UserSessionRepository : GenericRepository<Domain.Entities.UserSession, int>, Infrastructure.Abstractions.IRepositories.IUserSessionRepository
    {
        public UserSessionRepository(ApplicationContext context) : base(context)
        {

        }



    }
    
}
