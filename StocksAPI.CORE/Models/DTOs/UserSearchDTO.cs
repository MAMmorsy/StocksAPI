using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class UserSearchDTO
    {
        public int UserId { get; set; }
    }

    public class EncUserSearchDTO
    {
        public string UserId { get; set; }
    }
}
