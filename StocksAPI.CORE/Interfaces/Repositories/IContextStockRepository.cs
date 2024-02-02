﻿using StocksAPI.CORE.Interfaces.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.CORE.Interfaces.Repositories
{
    public interface IContextStockRepository<T>:IBaseRepository<T> where T : class
    {
    }
}
