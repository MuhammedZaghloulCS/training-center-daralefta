using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class RoomRepository : GenericRepository<Domain.Entities.Room, int>, Infrastructure.Abstractions.IRepositories.IRoomRepository
    {
        public RoomRepository(Infrastructure.Context.ApplicationContext context) : base(context)
        {
        }

    }
}
