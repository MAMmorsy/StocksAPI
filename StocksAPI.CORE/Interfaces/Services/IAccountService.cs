using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Response<LoginUserDataDto>> LoginUser(LoginDto loginDto);
        Task<Response<bool>> CreateUser(UserCreateDTO createDTO);
        Task<Response<bool>> UpdateUser(UserUpdateDTO updateDto);
        Task<Response<UserDataDTO>> GetUser(UserSearchDTO userSearchDto);
        Task<Response<List<UserDataDTO>>> GetUsersList(UserListSearchDTO listSearchDto);
    }
}
