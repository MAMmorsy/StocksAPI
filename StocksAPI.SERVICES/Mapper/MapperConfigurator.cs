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
                UserDataMapper.ConfigureMapping(mapperConfigs);

            });

            return mapperConfiguration;
        }
    }
}
