using del.Models;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysDoorsRepository : GenericSysRepository<acc_door>, ISysDoorsRepository
    {
        public SysDoorsRepository(security_dbContext context) : base(context)
        {
        }
    }
}
