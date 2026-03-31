using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class UserCourseRepository : GenericRepository<Domain.Entities.UsersCourse, int>, Infrastructure.Abstractions.IRepositories.IUserCourseRepository
    {
        public UserCourseRepository(ApplicationContext context) : base(context)
        {

        }



    }
}
