using AutoMapper;
using StocksAPI.SERVICES.Mapper;

namespace StocksAPI.API.Utilities.DependenciesResolver
{
    public class MapperDependenciesResolver
    {
        public static void Register(IServiceCollection services)
        {
            var configuraion = MapperConfigurator.ConfigureMappings();
            services.AddSingleton<IMapper>(new Mapper(configuraion));

        }
    }
}
