using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class ProductQuantitySearchDTO
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int UnitId { get; set; }
    }
    public class EncProductQuantitySearchDTO
    {
        public string ProductId { get; set; }
        public string StoreId { get; set; }
        public string UnitId { get; set; }
    }
}
