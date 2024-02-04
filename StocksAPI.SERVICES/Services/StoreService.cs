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
    public class StoreService : IStoreService
    {
        private readonly IContextStockRepository<Store> _storeRepository;
        private readonly IContextStockRepository<ProductUnit> _productUnitRepository;
        public IMapper _mapper;
        public StoreService(IMapper mapper, IContextStockRepository<Store> storeRepository, IContextStockRepository<ProductUnit> productUnitRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _productUnitRepository=productUnitRepository;
        }

        public async Task<Response<string>> GetStoreProductId(StoreProductSearchDTO storeProductSearchDTO)
        {
            Response<string> response = new Response<string>();
            try
            {
                response.Errors = ValidatorHandler.Validate(storeProductSearchDTO, (StoreProductSearchValidator)Activator.CreateInstance(typeof(StoreProductSearchValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }

                string[] includes = new string[] { "StoreProducts" };
                ProductUnit? unit = (ProductUnit?)await _productUnitRepository.FindAsync(e =>
                e.StoreProducts.Where(s => s.StoreId==storeProductSearchDTO.StoreId).Any()&&
                e.ProductId==storeProductSearchDTO.ProductId && 
                e.UnitId==storeProductSearchDTO.UnitId && e.Deleted==false, includes);

                if (unit == null)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No data found" });
                    response.IsSucceded = true;
                    return response;
                }
                
                response.Data = unit.StoreProducts.FirstOrDefault().StoreProductId.ToString();
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

        public async Task<Response<List<StoreListDTO>>> GetStoresList()
        {
            Response<List<StoreListDTO>> response = new Response<List<StoreListDTO>>();
            try
            {

                Expression<Func<Store, bool>> condition = e => e.Deleted==false;

                List<Store>? storeList = (List<Store>?)await _storeRepository.FindAllAsync(condition, null, null, s => s.StoreName, OrderBy.Ascending);
                if (storeList == null || storeList.Count == 0)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No stores found" });
                    response.IsSucceded = true;
                    return response;
                }
                var storeListDto = _mapper.Map<List<Store>, List<StoreListDTO>>(storeList);
                response.Data = storeListDto;
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
