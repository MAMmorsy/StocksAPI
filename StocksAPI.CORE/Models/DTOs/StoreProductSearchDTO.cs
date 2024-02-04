using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class StoreProductSearchDTO
    {
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public int StoreId { get; set; }
    }
    public class EncStoreProductSearchDTO
    {
        public string ProductId { get; set; }
        public string UnitId { get; set; }
        public string StoreId { get; set; }
    }
}
