using del.Models;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysAccessLevelPersonRepository : GenericSysRepository<acc_level_person>, ISysAccessLevelPersonRepository
    {
        public SysAccessLevelPersonRepository(security_dbContext context) : base(context)
        {
        }
    }
}
