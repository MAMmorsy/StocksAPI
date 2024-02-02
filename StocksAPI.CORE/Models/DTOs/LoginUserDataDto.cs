using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class LoginUserDataDto
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }

    public class LoginResponseDto:LoginUserDataDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
