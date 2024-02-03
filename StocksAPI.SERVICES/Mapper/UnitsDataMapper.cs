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
    public class UnitsDataMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<ProductUnit, UnitsListDTO>()
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.UnitName))
                .ReverseMap();
        }
    }
}
