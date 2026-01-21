using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class CourseRepository : GenericRepository<Domain.Entities.Course, int>, Infrastructure.Abstractions.IRepositories.ICourseRepository
    {
        public CourseRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }
    
    }
}
