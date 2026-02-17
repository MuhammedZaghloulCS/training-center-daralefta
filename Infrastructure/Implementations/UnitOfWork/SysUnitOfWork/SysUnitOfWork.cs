using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Context;
using Infrastructure.Implementations.Repository.SysRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.UnitOfWork.SysUnitOfWork
{
    public class SysUnitOfWork : ISysUnitOfWork
    {
        #region Fields
        private  ISysPersonRepository _sysPersonRepository;
        private ISysTimeSessionRepository _sysTimeSessionRepository;
        private ISysAccessLevelRepository _sysAccessLevelRepository;

        private readonly security_dbContext _context;
        #endregion

        public SysUnitOfWork(security_dbContext context)
        {
            _context = context;
        }


        public ISysPersonRepository ISysPersonRepository
        {
            get
            {
                if (_sysPersonRepository == null)
                {
                    _sysPersonRepository = new SysPersonRepository(_context);
                }
                return _sysPersonRepository;
            }
        }
        public ISysTimeSessionRepository ISysTimeSessionRepository { get
            {
                if ( _sysTimeSessionRepository == null)
                    _sysTimeSessionRepository = new SysTimeSessionRepository(_context);
                return _sysTimeSessionRepository;

            }
        }
        public ISysAccessLevelRepository ISysAccessLevelRepository{ get
            {
                if ( _sysAccessLevelRepository == null)
                    _sysAccessLevelRepository = new SysAccessLevelRepository(_context);
                return _sysAccessLevelRepository;

            }
        }

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
