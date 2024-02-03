using AutoMapper;
using StocksAPI.CORE.Enums;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.CORE.Models.Entities;
using StocksAPI.SERVICES.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Services
{
    public class UnitService : IUnitService
    {
        private readonly IContextStockRepository<ProductUnit> _unitRepository;
        public IMapper _mapper;
        public UnitService(IMapper mapper, IContextStockRepository<ProductUnit> unitRepository)
        {
            _mapper = mapper;
            _unitRepository = unitRepository;
        }
        public async Task<Response<List<UnitsListDTO>>> GetUnitsByProductId(UnitSearchDTO unitSearchDto)
        {
            Response<List<UnitsListDTO>> response = new Response<List<UnitsListDTO>>();
            try
            {
                response.Errors = ValidatorHandler.Validate(unitSearchDto, (UnitSearchValidator)Activator.CreateInstance(typeof(UnitSearchValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }

                string[] includes = new string[] { "Unit" };
                List<ProductUnit>? unitsList = (List<ProductUnit>?)await _unitRepository.FindAllAsync(e => e.StoreProducts.Where(s=>s.StoreId==unitSearchDto.StoreId).Any()&& e.ProductId==unitSearchDto.ProductId && e.Deleted==false, includes);
                
                if (unitsList == null || unitsList.Count == 0)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No units found" });
                    response.IsSucceded = true;
                    return response;
                }
                var unitListDto = _mapper.Map<List<ProductUnit>, List<UnitsListDTO>>(unitsList);
                response.Data = unitListDto;
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
