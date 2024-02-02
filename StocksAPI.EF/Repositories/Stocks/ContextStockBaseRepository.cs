using StocksAPI.CORE.Helpers;
using StocksAPI.CORE.Interfaces.Repositories.Shared;
using StocksAPI.EF.Context;
using StocksAPI.EF.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace StocksAPI.EF.Repositories.Stocks
{
    public class ContextStockBaseRepository<T> : BaseRepository<T> where T : class
    {
        protected InvoiceDbContext _context;

        public ContextStockBaseRepository(InvoiceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
