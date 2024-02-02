using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            ProductUnits = new HashSet<ProductUnit>();
        }

        [Key]
        public int UnitId { get; set; }
        [StringLength(100)]
        public string? UnitName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [InverseProperty("Unit")]
        public virtual ICollection<ProductUnit> ProductUnits { get; set; }
    }
}
