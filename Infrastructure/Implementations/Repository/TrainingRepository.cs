using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class TrainingRepository : GenericRepository<Domain.Entities.Training, int>, Infrastructure.Abstractions.IRepositories.ITrainingRepository
    {
        public TrainingRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }
    
    }
}
