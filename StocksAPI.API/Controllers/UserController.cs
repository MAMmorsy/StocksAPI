using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StocksAPI.API.Utilities;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StocksAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Stocks")]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;
        public UserController(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }

        // Done
        [AllowAnonymous]
        [HttpGet(Name = "UserLogin")]
        public async Task<IActionResult> UserLogin([FromHeader] string userName, [FromHeader] string password)
        {
            userName = EncryptionHelper.DecryptString(userName, _config.GetValue<string>("Pass"));
            password = EncryptionHelper.DecryptString(password, _config.GetValue<string>("Pass"));
            Response<LoginResponseDto> userLoginResponseDto = new Response<LoginResponseDto>();
            try
            {
                LoginDto loginDto = new LoginDto { UserName = userName, Password=password };
                Response<LoginUserDataDto> userDetailsDto = await _accountService.LoginUser(loginDto);
                userLoginResponseDto.ResponseCode = userDetailsDto.ResponseCode;
                userLoginResponseDto.IsSucceded = userDetailsDto.IsSucceded;
                userLoginResponseDto.Errors = userDetailsDto.Errors;
                userLoginResponseDto.SuccessObjCount = userDetailsDto.SuccessObjCount;
                string token = "";
                if (userDetailsDto.IsSucceded)
                {
                    var details = userDetailsDto.Data;
                    token = GenerateToken(details.UserId.ToString(), details.RoleName);
                    LoginResponseDto loginResponseDto = new LoginResponseDto
                    {
                        Token = token,
                        UserId = details.UserId
                    };
                    userLoginResponseDto.IsSucceded = true;
                    userLoginResponseDto.Data = loginResponseDto;
                }

                return Ok(userLoginResponseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[Authorize]
        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> GetUser([FromHeader] string sendData)
        {
            try
            {
                //var generatedcsResponce = JsonConvert.DeserializeObject<EncUserSearchDTO>(sendData);
                //string userId = generatedcsResponce.UserId;
                //string decUserId = EncryptionHelper.DecryptString(userId, _config.GetValue<string>("Pass"));
                //UserSearchDTO userSearchDto = new UserSearchDTO
                //{
                //    UserId = Convert.ToInt32(decUserId)
                //};
                UserSearchDTO userSearchDto = new UserSearchDTO
                {
                    UserId = Convert.ToInt32(sendData)
                };
                Response<UserDataDTO> userDetailsDto = await _accountService.GetUser(userSearchDto);
                return Ok(userDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        //[Authorize]
        [HttpGet(Name = "GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromHeader] string sendData)
        {
            try
            {
                //var generatedcsResponce = JsonConvert.DeserializeObject<EncUserListSearchDTO>(sendData);
                //string roleId = generatedcsResponce.RoleId;
                //string decRoleId = EncryptionHelper.DecryptString(roleId, _config.GetValue<string>("Pass"));
                //UserListSearchDTO userSearchDto = new UserListSearchDTO
                //{
                //    RoleId = Convert.ToInt32(decRoleId)
                //};
                UserListSearchDTO userSearchDto = new UserListSearchDTO
                {
                    RoleId = Convert.ToInt32(sendData)
                };
                Response<List<UserDataDTO>> userDetailsDto = await _accountService.GetUsersList(userSearchDto);
                return Ok(userDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [Authorize]
        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] string sendData)
        {
            try
            {
                string decSentData = EncryptionHelper.DecryptString(sendData, _config.GetValue<string>("Pass"));
                var userCreateDto = JsonConvert.DeserializeObject<UserCreateDTO>(decSentData);
                Response<bool> response = await _accountService.CreateUser(userCreateDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [Authorize]
        [HttpPost(Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] string sendData)
        {
            try
            {
                string decSentData = EncryptionHelper.DecryptString(sendData, _config.GetValue<string>("Pass"));
                var userUpdateDto = JsonConvert.DeserializeObject<UserUpdateDTO>(decSentData);
                Response<bool> response = await _accountService.CreateUser(userUpdateDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [NonAction]
        private string GenerateToken(string userId, string roleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserId",userId),
                new Claim("RoleName",roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
