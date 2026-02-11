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


        public async Task AddRangeIfNotExistsAsync(List<UsersTrainings> userTrainings)
        {
            if (userTrainings == null || !userTrainings.Any())
                return;

            var userId = userTrainings.First().UserId;
            var trainingIds = userTrainings.Select(x => x.TrainingId).ToList();

            var existing = await _context.UsersTrainings
                .Where(x => x.UserId == userId &&
                            trainingIds.Contains(x.TrainingId))
                .ToListAsync();

            var toAdd = userTrainings
                .Where(x => !existing.Any(e =>
                    e.UserId == x.UserId &&
                    e.TrainingId == x.TrainingId))
                .ToList();

            if (toAdd.Any())
                await _context.UsersTrainings.AddRangeAsync(toAdd);
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

        public async Task<List<Guid>> GetUsersIdsByTrainingId(int trainingId)
        {
            var usersIds=await _context.UsersTrainings.Where(ut => ut.TrainingId == trainingId)
                .Select(ut => ut.UserId)
                .ToListAsync();
            return usersIds;
        }
        public async Task<List<int>> GetTrainingIdsByUserId(Guid userId)
        {
            var trainingIds = await _context.UsersTrainings.Where(ut => ut.UserId == userId)
                .Select(ut => ut.TrainingId)
                .ToListAsync();
            return trainingIds;
        }

    }

}
