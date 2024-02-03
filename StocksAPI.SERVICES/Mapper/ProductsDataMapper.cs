using AutoMapper;
using StocksAPI.CORE.Models.Entities;
using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Mapper
{
    public class ProductsDataMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<StoreProduct, ProductsListDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductUnit.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductUnit.Product.ProductName))
                .ReverseMap();
            mapperConfigs.CreateMap<StoreProduct, ProductQuantityListDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductUnit.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();
        }
    }
}
