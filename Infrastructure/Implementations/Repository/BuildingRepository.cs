using Domain.Entities;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class BuildingRepository : GenericRepository<Building, int>, IBuildingRepository
    {
        public BuildingRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
