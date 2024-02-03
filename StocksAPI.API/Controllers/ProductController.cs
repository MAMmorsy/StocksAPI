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
    //[Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Stocks")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _config;
        public ProductController(IProductService productService, IConfiguration config)
        {
            _productService = productService;
            _config = config;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts([FromHeader] string sendData)
        {
            try
            {
                //var generatedcsResponce = JsonConvert.DeserializeObject<EncProductSearchDTO>(sendData);
                //string storeId = generatedcsResponce.StoreId;
                //string decStoreId = EncryptionHelper.DecryptString(storeId, _config.GetValue<string>("Pass"));
                //ProductSearchDTO productSearchDTO = new ProductSearchDTO
                //{
                //    StoreId = Convert.ToInt32(decStoreId)
                //};
                ProductSearchDTO productSearchDTO = new ProductSearchDTO
                {
                    StoreId = Convert.ToInt32(sendData)
                };
                Response<List<ProductsListDTO>> productDetailsDto = await _productService.GetProductsByStockId(productSearchDTO);
                return Ok(productDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
