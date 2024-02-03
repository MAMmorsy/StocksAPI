using AutoMapper;
using StocksAPI.CORE.Enums;
using StocksAPI.CORE.Helpers;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.CORE.Models.Entities;
using StocksAPI.SERVICES.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Services
{
    public class ProductService : IProductService
    {
        private readonly IContextStockRepository<StoreProduct> _productRepository;
        public IMapper _mapper;
        public ProductService(IMapper mapper, IContextStockRepository<StoreProduct> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Response<ProductQuantityListDTO>> GetProductQuantityByStockAndUnit(ProductQuantitySearchDTO productQuantitySearchDto)
        {
            Response<ProductQuantityListDTO> response = new Response<ProductQuantityListDTO>();
            try
            {
                response.Errors = ValidatorHandler.Validate(productQuantitySearchDto, (ProductQuantitySearchValidator)Activator.CreateInstance(typeof(ProductQuantitySearchValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }

                string[] includes = new string[] { "ProductUnit", "ProductUnit.Product" };
                StoreProduct? productsList = (StoreProduct?)await _productRepository.FindAsync
                    (e => e.StoreId==productQuantitySearchDto.StoreId
                && e.Deleted==false
                && e.ProductUnit.ProductId==productQuantitySearchDto.ProductId
                && e.ProductUnit.UnitId==productQuantitySearchDto.UnitId, includes);
                if (productsList == null)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No products found" });
                    response.IsSucceded = true;
                    return response;
                }
                var productListDto = _mapper.Map<StoreProduct, ProductQuantityListDTO>(productsList);
                response.Data = productListDto;
                response.ResponseCode = (int)ResponseCodesEnum.SuccessWithData;
            }
            catch (Exception ex)
            {
                response.ResponseCode = (int)ResponseCodesEnum.DbException;
                response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                response.IsSucceded = false;
            }
            return response;
        }

        public async Task<Response<List<ProductsListDTO>>> GetProductsByStockId(ProductSearchDTO productSearchDto)
        {
            Response<List<ProductsListDTO>> response = new Response<List<ProductsListDTO>>();
            try
            {
                response.Errors = ValidatorHandler.Validate(productSearchDto, (ProductSearchValidator)Activator.CreateInstance(typeof(ProductSearchValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                
                string [] includes= new string[] { "ProductUnit", "ProductUnit.Product" };
                List<StoreProduct>? productsList = (List<StoreProduct>?)await _productRepository.FindAllAsync(e => e.StoreId==productSearchDto.StoreId && e.Deleted==false, includes);
                if (productsList == null || productsList.Count == 0)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No products found" });
                    response.IsSucceded = true;
                    return response;
                }
                var productListDto = _mapper.Map<List<StoreProduct>, List<ProductsListDTO>>(productsList);
                response.Data = (List<ProductsListDTO>?)productListDto.DistinctBy(c=>new  { c.ProductId, c.ProductName}).ToList();
                response.ResponseCode = (int)ResponseCodesEnum.SuccessWithData;
            }
            catch (Exception ex)
            {
                response.ResponseCode = (int)ResponseCodesEnum.DbException;
                response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                response.IsSucceded = false;
            }
            return response;
        }
    }
}
