using del.Models;
using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository.SysRepository
{
    public class SysPersonRepository : GenericSysRepository<pers_person>, ISysPersonRepository
    {
        private readonly security_dbContext _context;
        public SysPersonRepository(security_dbContext context) : base(context)
        {
            this._context = context;
        }


        public async Task<pers_person> GetPersonByPinAsync(string pin)
        {
            return await _context.pers_people.FirstOrDefaultAsync(p => p.pin == pin);
        }
    }
}
