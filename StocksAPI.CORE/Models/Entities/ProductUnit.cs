using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class ProductUnit
    {
        [Key]
        public int ProductUnitId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductUnits")]
        public virtual Product Product { get; set; } = null!;
        [ForeignKey("UnitId")]
        [InverseProperty("ProductUnits")]
        public virtual Unit Unit { get; set; } = null!;
    }
}
