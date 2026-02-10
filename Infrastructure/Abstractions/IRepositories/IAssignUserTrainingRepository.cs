using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IAssignUserTrainingRepository
    {
        public  Task AddIfNotExistsAsync(UsersTrainings userTraining);


        public void Delete(UsersTrainings userTraining);
        
    }
}
