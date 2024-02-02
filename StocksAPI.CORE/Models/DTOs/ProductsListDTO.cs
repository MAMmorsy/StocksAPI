using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class ProductsListDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalQuantity { get; set; }
    }
}
