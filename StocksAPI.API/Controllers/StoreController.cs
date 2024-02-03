using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;

namespace StocksAPI.API.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Stocks")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IConfiguration _config;
        public StoreController(IStoreService storeService, IConfiguration config)
        {
            _storeService = storeService;
            _config = config;
        }

        [HttpGet(Name = "GetAllStores")]
        public async Task<IActionResult> GetAllStores()
        {
            try
            {
                Response<List<StoreListDTO>> storesDto = await _storeService.GetStoresList();
                return Ok(storesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
