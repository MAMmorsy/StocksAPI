using StocksAPI.CORE.Interfaces.Repositories.Shared;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.EF.Repositories.Shared
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private IDbContextTransaction _transaction;
        
        public DatabaseTransaction(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
        
    }
}
