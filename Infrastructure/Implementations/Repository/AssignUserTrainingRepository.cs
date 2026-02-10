using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class AssignUserTrainingRepository : IAssignUserTrainingRepository
    {
        private readonly ApplicationContext _context;

        public AssignUserTrainingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddIfNotExistsAsync(UsersTrainings userTraining)
        {
            var exists = await _context.UsersTrainings.AnyAsync(x =>
                x.UserId == userTraining.UserId &&
                x.TrainingId == userTraining.TrainingId);

            if (!exists)
                await _context.UsersTrainings.AddAsync(userTraining);
        }

        public void Delete(UsersTrainings userTraining)
        {
            _context.UsersTrainings.Remove(userTraining);
        }
    }

}
