using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Context;
using Infrastructure.Implementations.Repository.SysRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.UnitOfWork.SysUnitOfWork
{
    public class SysUnitOfWork : ISysUnitOfWork
    {
        #region Fields
        private  ISysPersonRepository _sysPersonRepository;
        private ISysTimeSessionRepository _sysTimeSessionRepository;
        private ISysAccessLevelRepository _sysAccessLevelRepository;
        private ISysDoorsRepository _sysDoorRepository;
        private ISysAccessLevelDoorRepository _sysAccessLevelDoorRepository;
        private ISysAccessLevelPersonRepository _sysAccessLevelPersonRepository;
        private ISysAuthAreaRepository _sysAuthAreaRepository;
        private ISysAccTransactionRepository _sysAccTransactionRepository;
        private readonly security_dbContext _context;
        private readonly ApplicationContext _applicationContext;
        #endregion

        public SysUnitOfWork(security_dbContext context, ApplicationContext applicationContext)
        {
            _context = context;
            _applicationContext = applicationContext;
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

        public ISysDoorsRepository ISysDoorRepository { get
            {
                if( _sysDoorRepository == null)
                    _sysDoorRepository=new SysDoorsRepository(_context);
                return (_sysDoorRepository);
            }
        }
        public ISysAccessLevelDoorRepository ISysAccessLevelDoorRepository
        {
            get
            {
                if(_sysAccessLevelDoorRepository == null)
                    _sysAccessLevelDoorRepository=new SysAccessLevelDoorRepository(_context);
                return _sysAccessLevelDoorRepository;
            }
        }
        public ISysAccessLevelPersonRepository ISysAccessLevelPersonRepository
        {
            get
            {
                if(_sysAccessLevelPersonRepository == null)
                    _sysAccessLevelPersonRepository = new SysAccessLevelPersonRepository(_context);
                return _sysAccessLevelPersonRepository;
            }
        }

        public ISysAuthAreaRepository ISysAuthAreaRepository
        {
            get {
                if( _sysAuthAreaRepository == null)
                    _sysAuthAreaRepository= new SysAuthAreaRepository(_context);
                return ( _sysAuthAreaRepository);
            }
        }

        public ISysAccTransactionRepository ISysAccTransactionRepository
        {
            get
            {
                if (_sysAccTransactionRepository == null)
                    _sysAccTransactionRepository = new SysAccTransactionRepository(_context, _applicationContext);
                return _sysAccTransactionRepository;
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
