using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class StoreProduct
    {
        [Key]
        public int StoreProductId { get; set; }
        public int StoreId { get; set; }
        public int ProductUnitId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("ProductUnitId")]
        [InverseProperty("StoreProducts")]
        public virtual ProductUnit ProductUnit { get; set; } = null!;
        [ForeignKey("StoreId")]
        [InverseProperty("StoreProducts")]
        public virtual Store Store { get; set; } = null!;
    }
}
