using del.Models;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysAccessLevelDoorRepository : GenericSysRepository<acc_level_door>, ISysAccessLevelDoorRepository
    {
        public SysAccessLevelDoorRepository(security_dbContext context) : base(context)
        {
        }
    }
}
