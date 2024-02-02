using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StocksAPI.API.Utilities;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.SERVICES.Services;

namespace StocksAPI.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Stocks")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IConfiguration _config;
        public RoleController(IRoleService roleService, IConfiguration config)
        {
            _roleService = roleService;
            _config = config;
        }

        [HttpGet(Name = "GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                Response<List<RoleListDTO>> rolesDto = await _roleService.GetRolesList();
                return Ok(rolesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
