using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class UnitSearchDTO
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
    }
    public class EncUnitSearchDTO
    {
        public string ProductId { get; set; }
        public string StoreId { get; set; }
    }
}
