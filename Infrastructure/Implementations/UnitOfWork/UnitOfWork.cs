using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        #region Fields

        private readonly ApplicationContext context;
        #endregion
        //CTOR
        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();

        }
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
