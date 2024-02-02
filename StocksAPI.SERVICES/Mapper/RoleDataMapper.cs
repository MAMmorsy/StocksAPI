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
    public class RoleDataMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<Role, RoleListDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ReverseMap();
        }
    }
}
