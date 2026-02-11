using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface ITrainingRepository: IGenericRepository<Training, int>
    {
        
    }
}
