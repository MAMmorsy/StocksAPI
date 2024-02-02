using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class InvoiceItem
    {
        [Key]
        public int InvoiceDetailsId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
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
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("InvoiceId")]
        [InverseProperty("InvoiceItems")]
        public virtual Invoice Invoice { get; set; } = null!;
        [ForeignKey("ProductId")]
        [InverseProperty("InvoiceItems")]
        public virtual Product Product { get; set; } = null!;
    }
}
