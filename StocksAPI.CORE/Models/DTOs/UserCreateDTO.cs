﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class UserCreateDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
