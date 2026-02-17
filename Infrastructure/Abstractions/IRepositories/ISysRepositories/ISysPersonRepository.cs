using del.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories.ISysRepositories
{
    public interface ISysPersonRepository : IGenericSysRepository<pers_person>
    {
        public Task<pers_person> GetPersonByPinAsync(string pin);
    }
}
