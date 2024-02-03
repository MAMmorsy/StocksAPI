using AutoMapper;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.SERVICES.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StocksAPI.CORE.Models;
using System.Linq.Expressions;
using StocksAPI.CORE.Enums;
using StocksAPI.CORE.Models.Entities;
using StocksAPI.CORE.Helpers;

namespace StocksAPI.SERVICES.Services
{
    public class AccountService : IAccountService
    {
        private readonly IContextStockRepository<User> _userRepository;
        private readonly IStockUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public AccountService(IMapper mapper,IStockUnitOfWork unitOfWork, IContextStockRepository<User> userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Response<LoginUserDataDto>> LoginUser(LoginDto loginDto)
        {
            Response<LoginUserDataDto> response = new Response<LoginUserDataDto>();
            try
            {
                response.Errors = ValidatorHandler.Validate(loginDto, (LoginValidator)Activator.CreateInstance(typeof(LoginValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                Expression<Func<User, bool>> condition = e => e.UserName == loginDto.UserName && e.Password==loginDto.Password && e.Deleted==false;
                string[] includes = new string[] { "Role" };
                User? loginDataRecords = await _userRepository.FindAsync(condition, includes);
                if (loginDataRecords == null)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "Invalid username or password" });
                    response.IsSucceded = true;
                    return response;
                }
                var loginDataDto = _mapper.Map<User, LoginUserDataDto>(loginDataRecords);
                response.Data = loginDataDto;
                response.ResponseCode = (int)ResponseCodesEnum.SuccessWithData;
            }
            catch (Exception ex)
            {
                response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                response.IsSucceded = false;
                response.ResponseCode = (int)ResponseCodesEnum.DbException;
            }
            return response;
        }
        public async Task<Response<UserDataDTO>> GetUser(UserSearchDTO userSearchDto)
        {
            Response<UserDataDTO> response = new Response<UserDataDTO>();
            try
            {
                response.Errors = ValidatorHandler.Validate(userSearchDto, (UserSelectValidator)Activator.CreateInstance(typeof(UserSelectValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                Expression<Func<User, bool>> condition = e => e.UserId == userSearchDto.UserId && e.Deleted==false;
                string[] includes = new string[] { "Role" };
                User? userData = await _userRepository.FindAsync(condition, includes);
                if (userData == null)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No data found" });
                    response.IsSucceded = true;
                    return response;
                }
                var userDataDto = _mapper.Map<User, UserDataDTO>(userData);
                response.Data = userDataDto;
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

        public async Task<Response<List<UserDataDTO>>> GetUsersList(UserListSearchDTO listSearchDto)
        {
            Response<List<UserDataDTO>> response = new Response<List<UserDataDTO>>();
            try
            {
                if (listSearchDto==null)
                {
                    response.Errors =new List<Error> { new Error { ErrorMessage="Please fill user role" } };
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                Expression<Func<User, bool>> condition = e => e.Deleted==false;
                if (listSearchDto.RoleId!=0)
                {
                    condition = e => e.RoleId == listSearchDto.RoleId && e.Deleted == false;
                }
                string[] includes = new string[] { "Role" };
                List<User>? userList = (List<User>?)await _userRepository.FindAllAsync(condition, includes);
                if (userList == null || userList.Count == 0)
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "No users found" });
                    response.IsSucceded = true;
                    return response;
                }
                var userListDto = _mapper.Map<List<User>, List<UserDataDTO>>(userList);
                response.Data = userListDto;
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

        public async Task<Response<bool>> CreateUser(UserCreateDTO createDTO)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                response.Errors = ValidatorHandler.Validate(createDTO, (UserCreateValidator)Activator.CreateInstance(typeof(UserCreateValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                User user = _mapper.Map<User>(createDTO);
                Expression<Func<User, bool>> condition = e => e.UserName == createDTO.UserName && e.Deleted==false;
                // Check if user name existing before
                User? userData = await _userRepository.FindAsync(condition);
                if (userData == null)
                {
                    using (var transaction = _unitOfWork.BeginTransaction())
                    {
                        try
                        {
                            // Insert new Details
                            user.Datein = DateTime.Now;
                            user.Deleted = false;
                            await _userRepository.AddAsync(user);
                            response.Data = _unitOfWork.SaveChanges() >= 1;
                            response.Errors?.Add(new Error() { ErrorMessage = "User created." });
                            response.ResponseCode = (int)ResponseCodesEnum.SaveAllRecords;
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            response.ResponseCode = (int)ResponseCodesEnum.DbException;
                            response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                            response.IsSucceded = false;
                        }
                    }
                }
                else
                {
                    response.ResponseCode = (int)ResponseCodesEnum.ExistingData;
                    response.Errors?.Add(new Error() { ErrorMessage = "User Name already exists." });
                    response.IsSucceded = false;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = (int)ResponseCodesEnum.DbException;
                response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                response.IsSucceded = false;
            }
            return response;
        }

        public async Task<Response<bool>> UpdateUser(UserUpdateDTO updateDto)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                response.Errors = ValidatorHandler.Validate(updateDto, (UserUpdateValidator)Activator.CreateInstance(typeof(UserUpdateValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                User user = _mapper.Map<User>(updateDto);
                Expression<Func<User, bool>> condition = e => e.UserId == updateDto.UserId && e.Deleted == false;
                // Check if user name existing before
                User? userData = await _userRepository.FindAsync(condition);
                if (userData != null)
                {
                    using (var transaction = _unitOfWork.BeginTransaction())
                    {
                        try
                        {
                            // Update Details
                            _userRepository.Update(true, user, e => e.Datein);
                            response.Data = _unitOfWork.SaveChanges() >= 1;
                            response.Errors?.Add(new Error() { ErrorMessage = "User created." });
                            response.ResponseCode = (int)ResponseCodesEnum.SaveAllRecords;
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            response.ResponseCode = (int)ResponseCodesEnum.DbException;
                            response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                            response.IsSucceded = false;
                        }
                    }
                }
                else
                {
                    response.ResponseCode = (int)ResponseCodesEnum.SuccessWithoutData;
                    response.Errors?.Add(new Error() { ErrorMessage = "User not found" });
                    response.IsSucceded = false;
                }
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
