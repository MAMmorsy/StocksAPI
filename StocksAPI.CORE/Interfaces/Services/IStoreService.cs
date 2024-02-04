using StocksAPI.CORE.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Interfaces.Services
{
    public interface IStoreService
    {
        Task<Response<List<StoreListDTO>>> GetStoresList();
        Task<Response<string>> GetStoreProductId(StoreProductSearchDTO storeProductSearchDTO);
    }
}
