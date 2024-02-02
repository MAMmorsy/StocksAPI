using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
            ProductUnits = new HashSet<ProductUnit>();
            StoreProducts = new HashSet<StoreProduct>();
        }

        [Key]
        public int ProductId { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductUnit> ProductUnits { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
