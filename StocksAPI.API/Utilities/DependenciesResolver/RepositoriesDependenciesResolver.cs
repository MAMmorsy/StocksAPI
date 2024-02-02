//using Azure.Core;
using StocksAPI.CORE.Interfaces.Repositories.Shared;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.EF.Repositories.Shared;
using StocksAPI.EF.Repositories.Stocks;

namespace StocksAPI.API.Utilities.DependenciesResolver
{
    public class RepositoriesDependenciesResolver
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IStockUnitOfWork, StockUnitOfWork>();
            services.AddScoped(typeof(IContextStockRepository<>), typeof(ContextStockBaseRepository<>));


        }
    }
}
