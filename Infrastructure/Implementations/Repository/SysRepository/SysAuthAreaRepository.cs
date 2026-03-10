using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysAuthAreaRepository : GenericSysRepository<del.Models.auth_area>, ISysAuthAreaRepository
    {
        public SysAuthAreaRepository(security_dbContext context) : base(context)
        {
        }
    }
}
