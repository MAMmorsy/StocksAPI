using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [StringLength(50)]
        public string Password { get; set; } = null!;
        [StringLength(100)]
        public string DisplayName { get; set; } = null!;
        public int RoleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; } = null!;
        [InverseProperty("User")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
