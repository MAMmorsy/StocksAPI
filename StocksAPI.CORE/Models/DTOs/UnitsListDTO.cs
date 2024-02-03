using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class UnitsListDTO
    {
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
    }
}
