using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class InvoiceItemsDTO
    {
        public int InvoiceDetailsId { get; set; }
        public int InvoiceId { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Net { get; set; }
    }
}
