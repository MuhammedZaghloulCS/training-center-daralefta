using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysTimeSessionRepository : GenericSysRepository<del.Models.acc_timeseg>, ISysTimeSessionRepository
    {
        public SysTimeSessionRepository(security_dbContext context) : base(context)
        {
        }
    }
}
