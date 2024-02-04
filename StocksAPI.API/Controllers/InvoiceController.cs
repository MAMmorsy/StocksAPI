using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StocksAPI.API.Utilities;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.SERVICES.Services;
using System.Collections.Generic;

namespace StocksAPI.API.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Stocks")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IConfiguration _config;
        public InvoiceController(IInvoiceService invoiceService, IConfiguration config)
        {
            _invoiceService = invoiceService;
            _config = config;
        }


        [HttpPost(Name = "CreateInvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] string sendData)
        {
            try
            {
                string decSentData = EncryptionHelper.DecryptString(sendData, _config.GetValue<string>("Pass"));
                var invoiceCreateDto = JsonConvert.DeserializeObject<InvoiceCreateDTO>(decSentData);
                Response<bool> response = await _invoiceService.CreateInvoice(invoiceCreateDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            try
            {
                Response<List<InvoiceDataDTO>> response = await _invoiceService.ViewInvoices();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
