using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class UserListSearchDTO
    {
        public int RoleId { get; set; } = 0;
    }
    public class EncUserListSearchDTO
    {
        public string RoleId { get; set; } = "0";
    }
}
