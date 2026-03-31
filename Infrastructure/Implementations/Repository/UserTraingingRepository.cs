using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class UserTrainingRepository : GenericRepository<Domain.Entities.UsersTrainings, int>, Abstractions.IRepositories.IUserTrainingRepository
    {
        public UserTrainingRepository(ApplicationContext context) : base(context)
        {

        }



    }
}
