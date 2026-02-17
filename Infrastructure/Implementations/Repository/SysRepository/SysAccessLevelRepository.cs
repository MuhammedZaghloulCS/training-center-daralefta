using del.Models;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysAccessLevelRepository : GenericSysRepository<acc_level>, ISysAccessLevelRepository
    {
        public SysAccessLevelRepository(security_dbContext context) : base(context)
        {
        }
    }
}
