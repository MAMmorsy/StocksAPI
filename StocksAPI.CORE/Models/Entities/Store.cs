using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class Store
    {
        public Store()
        {
            StoreProducts = new HashSet<StoreProduct>();
        }

        [Key]
        public int StoreId { get; set; }
        [StringLength(50)]
        public string StoreName { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [InverseProperty("Store")]
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
    }
}
