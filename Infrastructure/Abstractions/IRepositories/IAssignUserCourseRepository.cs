using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IAssignUserCourseRepository
    {
        public  Task AddRangeIfNotExistsAsync(List<UsersCourse> userCourses);

        public  Task AddIfNotExistsAsync(UsersCourse userCourse);

        public void Delete(UsersCourse userCourse);
        
    }
}
