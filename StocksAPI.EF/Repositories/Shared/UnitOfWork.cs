using StocksAPI.CORE.Interfaces.Repositories.Shared;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.EF.Repositories.Shared
{
    public class UnitOfWork : IStockUnitOfWork
    {
        private readonly DbContext _context;
        

        //private IBaseRepository<CdeYear> _CdeYears;

        //public IBaseRepository<CdeYear> CdeYears => _CdeYears;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new DatabaseTransaction(_context);
        }
    }
}
