﻿using StocksAPI.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Models.DTOs
{
    public class Response<T>
    {
        public Response()
        {
            ResponseCode = (int)ResponseCodesEnum.SuccessWithData;
            IsSucceded = true;
            Errors = new List<Error>();
            SuccessObjCount = 0;
        }

        // Get data codes
        // 1 = Success & retrive data
        // 2 = Success & No Data found

        // Insert, Update, & Delete codes
        // 11 = All records succeded
        // 12 = Some records succeded

        // General Codes
        // 3 = Error in sent parameters
        // 4 = DB Exception
        public int ResponseCode { get; set; }

        // Number of success records
        public int? SuccessObjCount { get; set; }
        public T? Data { get; set; }
        public bool IsSucceded { get; set; }
        public List<Error>? Errors { get; set; }
    }
    public class Error
    {
        public string? ErrorMessage { get; set; } = null!;
    }
    public class Empty { }
}
