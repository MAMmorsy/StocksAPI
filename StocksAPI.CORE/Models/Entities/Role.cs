using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StocksAPI.CORE.Models.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }
    }
}
