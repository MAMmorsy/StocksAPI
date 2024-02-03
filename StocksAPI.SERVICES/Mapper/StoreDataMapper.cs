using AutoMapper;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.CORE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Mapper
{
    public class StoreDataMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<Store, StoreListDTO>()
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.StoreName))
                .ReverseMap();
        }
    }
}
