using StocksAPI.CORE.Interfaces.Repositories.Shared;
using StocksAPI.EF.Context;
using StocksAPI.EF.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.EF.Repositories.Stocks
{
    public class StockUnitOfWork : UnitOfWork
    {
        private readonly InvoiceDbContext _context;

        public StockUnitOfWork(InvoiceDbContext context):base(context)
        {
            _context = context;
        }

    }
}
