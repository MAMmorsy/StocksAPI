using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.SERVICES.Services;

namespace StocksAPI.API.Utilities.DependenciesResolver
{
    public class ServicesDependenciesResolver
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
