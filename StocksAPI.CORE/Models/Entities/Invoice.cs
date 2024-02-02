using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        [Key]
        public int InvoiceId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InvoiceNo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime InvoiceDate { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Taxes { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Net { get; set; }
        public int TotalItems { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalDiscount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Invoices")]
        public virtual User User { get; set; } = null!;
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
