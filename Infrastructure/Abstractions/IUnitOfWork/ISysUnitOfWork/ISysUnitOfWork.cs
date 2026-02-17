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
        ISysTimeSessionRepository ISysTimeSessionRepository { get; }
        ISysAccessLevelRepository ISysAccessLevelRepository { get; }
        #endregion
        Task<int> Complete();
        void Dispose();
    }
}
