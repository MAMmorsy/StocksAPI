using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StocksAPI.API.Utilities;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;

namespace StocksAPI.API.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Stocks")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IConfiguration _config;
        public UnitController(IUnitService unitService, IConfiguration config)
        {
            _unitService = unitService;
            _config = config;
        }

        [HttpGet(Name = "GetUnits")]
        public async Task<IActionResult> GetUnits([FromHeader] string sendData, [FromHeader] string sendData1)
        {
            try
            {
                //var generatedcsResponce = JsonConvert.DeserializeObject<EncUnitSearchDTO>(sendData);
                //string productId = generatedcsResponce.ProductId;
                //string decStoreId = EncryptionHelper.DecryptString(productId, _config.GetValue<string>("Pass"));
                //UnitSearchDTO unitSearchDTO = new UnitSearchDTO
                //{
                //    ProductId = Convert.ToInt32(decStoreId)
                //};
                UnitSearchDTO unitSearchDTO = new UnitSearchDTO
                {
                    ProductId = Convert.ToInt32(sendData),
                    StoreId = Convert.ToInt32(sendData1)
                };
                Response<List<UnitsListDTO>> unitDetailsDto = await _unitService.GetUnitsByProductId(unitSearchDTO);
                return Ok(unitDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
