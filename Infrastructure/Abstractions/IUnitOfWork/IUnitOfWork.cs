using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region properties
        
        #endregion
        Task<int> Complete();
    }
}
