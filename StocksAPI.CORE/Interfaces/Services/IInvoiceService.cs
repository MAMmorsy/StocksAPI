using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<Response<bool>> CreateInvoice(InvoiceCreateDTO createDTO);
        Task<Response<List<InvoiceDataDTO>>> ViewInvoices();
    }
}
