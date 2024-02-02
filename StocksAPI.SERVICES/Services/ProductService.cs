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
                //Expression<Func<Role, bool>> condition = e => e. e.Deleted==false;
                string [] includes= new string[] { "Product", "Store" };
                var products = await _productRepository.FindAllAsync(e => e.StoreId==productSearchDto.StoreId && e.Deleted==false, includes);
                if (products.ToList() == null || products.ToList().Count == 0)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No roles found" });
                    response.IsSucceded = true;
                    return response;
                }
                //var roleListDto = _mapper.Map<List<Role>, List<RoleListDTO>>(roleList);
                response.Data = null;
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
