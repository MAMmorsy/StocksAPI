using AutoMapper;
using StocksAPI.CORE.Enums;
using StocksAPI.CORE.Helpers;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.CORE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Services
{
    public class RoleService : IRoleService
    {
        private readonly IContextStockRepository<Role> _roleRepository;
        public IMapper _mapper;
        public RoleService(IMapper mapper, IContextStockRepository<Role> roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task<Response<List<RoleListDTO>>> GetRolesList()
        {
            Response<List<RoleListDTO>> response = new Response<List<RoleListDTO>>();
            try
            {

                Expression<Func<Role, bool>> condition = e => e.Deleted==false;
                
                List<Role>? roleList = (List<Role>?)await _roleRepository.FindAllAsync(condition, null, null, s => s.RoleName, OrderBy.Ascending);
                if (roleList == null || roleList.Count == 0)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No roles found" });
                    response.IsSucceded = true;
                    return response;
                }
                var roleListDto = _mapper.Map<List<Role>, List<RoleListDTO>>(roleList);
                response.Data = roleListDto;
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
