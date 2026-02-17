using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork
{
    public interface ISysUnitOfWork : IDisposable
    {
        #region Properties
        ISysPersonRepository ISysPersonRepository { get; }
        #endregion
        Task<int> Complete();
        void Dispose();
    }
}
