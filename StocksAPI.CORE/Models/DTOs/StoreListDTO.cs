﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class StoreListDTO
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
    }
}
