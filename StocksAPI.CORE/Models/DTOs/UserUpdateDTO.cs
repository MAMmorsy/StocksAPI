
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class UserUpdateDTO : UserCreateDTO
    {
        public int UserId { get; set; }
    }
}
