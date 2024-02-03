using AutoMapper;
using StocksAPI.SERVICES.Mapper;

namespace StocksAPI.SERVICES.Mapper
{
    public class MapperConfigurator
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var mapperConfiguration = new MapperConfiguration(mapperConfigs =>
            {
                ProductsDataMapper.ConfigureMapping(mapperConfigs);
                RoleDataMapper.ConfigureMapping(mapperConfigs);
                StoreDataMapper.ConfigureMapping(mapperConfigs);
                UnitsDataMapper.ConfigureMapping(mapperConfigs);
                UserDataMapper.ConfigureMapping(mapperConfigs);
                InvoiceDataMapper.ConfigureMapping(mapperConfigs);
            });
            return mapperConfiguration;
        }
    }
}
