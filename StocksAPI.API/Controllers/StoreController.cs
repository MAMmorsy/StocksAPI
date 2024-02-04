using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StocksAPI.API.Utilities;
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

        [HttpGet(Name = "GetStoreProducts")]
        public async Task<IActionResult> GetStoreProducts([FromHeader] string sendData)
        {
            try
            {
                var generatedcsResponce = JsonConvert.DeserializeObject<EncStoreProductSearchDTO>(sendData);
                string storeId = generatedcsResponce.StoreId;
                string productId = generatedcsResponce.ProductId;
                string unitId = generatedcsResponce.UnitId;
                string decStoreId = EncryptionHelper.DecryptString(storeId, _config.GetValue<string>("Pass"));
                string decProductId = EncryptionHelper.DecryptString(productId, _config.GetValue<string>("Pass"));
                string decUnitId = EncryptionHelper.DecryptString(unitId, _config.GetValue<string>("Pass"));
                StoreProductSearchDTO productSearchDTO = new StoreProductSearchDTO
                {
                    StoreId = Convert.ToInt32(decStoreId),
                    ProductId = Convert.ToInt32(decProductId),
                    UnitId = Convert.ToInt32(decUnitId)
                };
                Response<string> storesDto = await _storeService.GetStoreProductId(productSearchDTO);
                return Ok(storesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
