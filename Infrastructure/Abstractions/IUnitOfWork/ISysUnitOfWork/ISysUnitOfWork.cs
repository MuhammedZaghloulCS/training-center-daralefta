using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Implementations.Repository.SysRepository;
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
        ISysDoorsRepository ISysDoorRepository { get; }
        ISysAccessLevelDoorRepository ISysAccessLevelDoorRepository { get; }
        ISysAccessLevelPersonRepository ISysAccessLevelPersonRepository { get; }

        #endregion
        Task<int> Complete();
        void Dispose();
    }
}
