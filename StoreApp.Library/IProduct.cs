﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public interface IProduct : IIdentifiable
    {
        string Name { get; }

        decimal Price { get; }
    }
}
